using System;
using System.Threading.Tasks;
using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using FoundersPC.UI.Admin.Services;
using MediatR;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class UserDetailsPageViewModel : BindableBase
{
    private readonly TitleBarLocator _titleBarLocator;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly NotificationHost _notificationHost;

    public UserDetailsPageViewModel(TitleBarLocator titleBarLocator,
                                    IMediator mediator,
                                    IMapper mapper,
                                    SelectedObjectLocator selectedObjectLocator,
                                    NotificationHost notificationHost)
    {
        _titleBarLocator = titleBarLocator;
        _mediator = mediator;
        _mapper = mapper;
        _notificationHost = notificationHost;
        selectedObjectLocator.SelectedUserChanged += OnSelectedUserChanged;
        _titleBarLocator.IsLoadingChanged += _ =>
                                             {
                                                 GoBackCommand.RaiseCanExecuteChanged();
                                             };
    }

    private void OnSelectedUserChanged(UserInfo? obj)
    {
        if (obj == null)
            EditableUser = null;

        EditableUser = _mapper.Map<UserInfoViewModel>(obj);
    }

    #region IsUpdating

    private bool _isUpdating;
    public bool IsUpdating
    {
        get => _isUpdating;
        set
        {
            SetProperty(ref _isUpdating, value);
            GoBackCommand.RaiseCanExecuteChanged();
            BlockAccessTokenCommand.RaiseCanExecuteChanged();
            UnblockAccessTokenCommand.RaiseCanExecuteChanged();
        }
    }

    private void ChangeLoadingState(bool state)
    {
        if (state == IsUpdating)
            return;

        if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            IsUpdating = state;
        else
            System.Windows.Application.Current.Dispatcher.Invoke(() => IsUpdating = state);
    }

    #endregion

    #region EditableUser

    private UserInfoViewModel? _editableUser;
    public UserInfoViewModel? EditableUser
    {
        get => _editableUser;
        set
        {
            SetProperty(ref _editableUser, value);
            GoBackCommand.RaiseCanExecuteChanged();
        }
    }

    #endregion

    #region GoBackCommand

    private MvxCommand? _goBackCommand;
    public MvxCommand GoBackCommand =>
        _goBackCommand ??= new(GoBack,
                               CanGoBack);

    private void GoBack()
    {
        EditableUser = null;
        _titleBarLocator.CurrentFrameId = TitleBarConstants.UsersPageId;
    }

    private bool CanGoBack()
    {
        if (IsUpdating)
            return false;

        return !_titleBarLocator.IsLoading;
    }

    #endregion

    #region BlockAccessTokenCommand

    private MvxAsyncCommand<AccessTokenViewModel>? _blockAccessTokenCommand;

    public MvxAsyncCommand<AccessTokenViewModel> BlockAccessTokenCommand =>
        _blockAccessTokenCommand ??= new(BlockAccessTokenAsync, _ => true, true);

    private async Task BlockAccessTokenAsync(AccessTokenViewModel accessToken)
    {
        accessToken.IsBlocked = true;
        ChangeLoadingState(true);
        _titleBarLocator.IsLoading = true;

        try
        {
            await _mediator.Send(new Application.Features.AccessToken.BlockRequest { Id = accessToken.Id });
        }
        catch (Exception e)
        {
            _notificationHost.ShowExceptionNotification(e);
        }

        _titleBarLocator.IsLoading = false;
        ChangeLoadingState(false);
    }

    #endregion

    #region UnblockAccessTokenCommand

    private MvxAsyncCommand<AccessTokenViewModel>? _unblockAccessTokenCommand;
    public MvxAsyncCommand<AccessTokenViewModel> UnblockAccessTokenCommand =>
        _unblockAccessTokenCommand ??= new(UnblockAccessTokenAsync, _ => true, true);

    private async Task UnblockAccessTokenAsync(AccessTokenViewModel accessToken)
    {
        accessToken.IsBlocked = false;
        ChangeLoadingState(true);
        _titleBarLocator.IsLoading = true;

        try
        {
            await _mediator.Send(new Application.Features.AccessToken.UnblockRequest { Id = accessToken.Id });
        }
        catch (Exception e)
        {
            _notificationHost.ShowExceptionNotification(e);
        }

        _titleBarLocator.IsLoading = false;
        ChangeLoadingState(false);
    }

    #endregion
}