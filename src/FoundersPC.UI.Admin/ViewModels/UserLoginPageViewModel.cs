using System;
using System.Threading.Tasks;
using Amusoft.UI.WPF.Notifications;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.SharedKernel.Interfaces;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Services;
using Microsoft.AspNetCore.Identity;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class UserLoginPageViewModel : BindableBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly MainWindowTitleBarLocator _mainWindowTitleBarLocator;
    private readonly TitleBarLocator _titleBarLocator;
    private readonly ICurrentUserService _currentUserService;
    private readonly NotificationHost _notificationHost;

    public UserLoginPageViewModel(UserManager<ApplicationUser> userManager,
                                  IPasswordHasher<ApplicationUser> passwordHasher,
                                  MainWindowTitleBarLocator mainWindowTitleBarLocator,
                                  TitleBarLocator titleBarLocator,
                                  ICurrentUserService currentUserService,
                                  NotificationHost notificationHost)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _mainWindowTitleBarLocator = mainWindowTitleBarLocator;
        _titleBarLocator = titleBarLocator;
        _currentUserService = currentUserService;
        _notificationHost = notificationHost;
    }

    private string? _password = "123456";
    public string? Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            SignInCommand.RaiseCanExecuteChanged();
        }
    }

    private string? _loginOrEmail = "system";
    public string? LoginOrEmail
    {
        get => _loginOrEmail;
        set
        {
            SetProperty(ref _loginOrEmail, value);
            SignInCommand.RaiseCanExecuteChanged();
        }
    }

    private MvxAsyncCommand? _signInCommand;
    public MvxAsyncCommand SignInCommand =>
        _signInCommand ??= new(SignInAsync,
                               () => !String.IsNullOrEmpty(LoginOrEmail)
                                     && !String.IsNullOrEmpty(Password)
                                     && LoginOrEmail.Length >= 5
                                     && Password.Length >= 6);

    private async Task SignInAsync()
    {
        _notificationHost.ShowInformationNotification("Collecting data..");

        var user = await GetUserAsync();

        if (user == null)
        {
            LoginOrEmail = null;
            Password = null;
            _notificationHost.ShowInformationNotification($"Not found user with login or email {LoginOrEmail}");

            return;
        }

        if (_passwordHasher.VerifyHashedPassword(user,
                                                 _passwordHasher.HashPassword(user, Password),
                                                 Password)
            != PasswordVerificationResult.Success)
        {
            Password = null;
            _notificationHost.ShowInformationNotification("Not right password!");

            return;
        }

        _notificationHost.ShowInformationNotification("Setting current user..");
        _currentUserService.Initialize(user.Id);
        _notificationHost.ShowInformationNotification("Almost done! Just redirecting you at Admin app..");
        _mainWindowTitleBarLocator.CurrentFrameId = MainWindowTitleBarConstants.AfterSignInPageId;
        _titleBarLocator.CurrentFrameId = TitleBarConstants.CasesPageId;
    }

    private async Task<ApplicationUser?> GetUserAsync()
    {
        var byemail = await _userManager.FindByEmailAsync(LoginOrEmail);

        if (byemail != null)
            return byemail;

        return await _userManager.FindByNameAsync(LoginOrEmail);
    }
}