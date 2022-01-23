using System;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.SharedKernel.Interfaces;
using FoundersPC.SharedKernel.Models;
using FoundersPC.UI.Admin.Locators;
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

    public UserLoginPageViewModel(UserManager<ApplicationUser> userManager,
                                  IPasswordHasher<ApplicationUser> passwordHasher,
                                  MainWindowTitleBarLocator mainWindowTitleBarLocator,
                                  TitleBarLocator titleBarLocator,
                                  ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _mainWindowTitleBarLocator = mainWindowTitleBarLocator;
        _titleBarLocator = titleBarLocator;
        _currentUserService = currentUserService;
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

    private Error? _error;
    public Error? Error
    {
        get => _error;
        set => SetProperty(ref _error, value);
    }

    private async Task SignInAsync()
    {
        Error = null;
        RefreshLocator.FireMessaging(true, "Collecting data..");

        var user = await GetUserAsync();

        if (user == null)
        {
            Error = new("Not found user", $"Not found user with Login or Email {LoginOrEmail}");
            LoginOrEmail = null;
            Password = null;
            RefreshLocator.FireMessaging(false);

            return;
        }

        if (_passwordHasher.VerifyHashedPassword(user,
                                                 _passwordHasher.HashPassword(user, Password),
                                                 Password)
            != PasswordVerificationResult.Success)
        {
            Error = new("Password is incorrect", "Provided password is not correct. Try again");
            Password = null;
            RefreshLocator.FireMessaging(false);

            return;
        }

        RefreshLocator.FireMessaging(true, "Setting current user..");
        //await Task.Delay(1000);
        _currentUserService.Initialize(user.Id);
        RefreshLocator.FireMessaging(true, "Almost done! Just redirecting you at Admin app..");
        //await Task.Delay(5000);
        _mainWindowTitleBarLocator.CurrentFrameId = MainWindowTitleBarConstants.AfterSignInPageId;
        _titleBarLocator.CurrentFrameId = TitleBarConstants.CasesPageId;
        RefreshLocator.FireMessaging(false);
        RefreshLocator.FireSuccessLogIn();
    }

    private async Task<ApplicationUser?> GetUserAsync() =>
        await _userManager.FindByEmailAsync(LoginOrEmail) ?? await _userManager.FindByNameAsync(LoginOrEmail);
}