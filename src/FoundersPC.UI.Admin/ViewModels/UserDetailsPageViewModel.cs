using AutoMapper;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class UserDetailsPageViewModel : BindableBase
{
    private readonly TitleBarLocator _titleBarLocator;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserDetailsPageViewModel(TitleBarLocator titleBarLocator,
                                    IMediator mediator,
                                    IMapper mapper,
                                    SelectedObjectLocator selectedObjectLocator)
    {
        _titleBarLocator = titleBarLocator;
        _mediator = mediator;
        _mapper = mapper;
        selectedObjectLocator.SelectedUserChanged += OnSelectedUserChanged;
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

    #region SelectedToken

    private AccessTokenViewModel? _selectedToken;
    public AccessTokenViewModel? SelectedToken
    {
        get => _selectedToken;
        set
        {
            SetProperty(ref _selectedToken, value);
            GoBackCommand.RaiseCanExecuteChanged();
        }
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
                               () => !IsUpdating);

    private void GoBack()
    {
        EditableUser = null;
        _titleBarLocator.CurrentFrameId = TitleBarConstants.UsersPageId;
    }

    #endregion
}