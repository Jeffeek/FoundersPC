using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Services;
using MediatR;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class UsersPageViewModel : BindableBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public TitleBarLocator TitleBarLocator { get; }
    private readonly SelectedObjectLocator _selectedObjectLocator;
    private readonly NotificationHost _notificationHost;

    public UsersPageViewModel(IMediator mediator,
                              IMapper mapper,
                              TitleBarLocator titleBarLocator,
                              SelectedObjectLocator selectedObjectLocator,
                              FilterOptions filterOptions,
                              NotificationHost notificationHost)
    {
        _mediator = mediator;
        _mapper = mapper;
        TitleBarLocator = titleBarLocator;
        _selectedObjectLocator = selectedObjectLocator;
        _notificationHost = notificationHost;
        FilterOptions = filterOptions;
        TitleBarLocator.IsLoadingChanged += _ => ApplySearchCommand.RaiseCanExecuteChanged();

        OrderByList = new()
                      {
                          "Id",
                          "Email",
                          "Login",
                          "Role"
                      };

        PageSizeList = new()
                       {
                           10,
                           20,
                           50,
                           100
                       };

        titleBarLocator.TabChanged += async tabId =>
                                      {
                                          if (tabId != TitleBarConstants.UsersPageId)
                                              return;

                                          await SearchUsersAsync();
                                      };
    }

    private void ChangeLoadingState(bool state)
    {
        if (state == TitleBarLocator.IsLoading)
            return;

        if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            TitleBarLocator.IsLoading = state;
        else
            System.Windows.Application.Current.Dispatcher.Invoke(() => TitleBarLocator.IsLoading = state);
    }

    #region MoveToUserDetailsPageCommand

    private MvxAsyncCommand<UserViewInfo>? _moveToUserDetailsPageCommand;
    public MvxAsyncCommand<UserViewInfo> MoveToUserDetailsPageCommand =>
        _moveToUserDetailsPageCommand ??= new(x => MoveToUserDetailsPageAsync(x.Id));

    private async Task MoveToUserDetailsPageAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        var userInfo = await _mediator.Send(new Application.Features.UserInformation.GetRequest { Id = id });
        _selectedObjectLocator.SelectedUser = userInfo;
        TitleBarLocator.CurrentFrameId = TitleBarConstants.UserDetailsPageId;
        TitleBarLocator.IsLoading = false;
    }

    #endregion

    #region BlockUserCommand

    private MvxAsyncCommand<UserViewInfo>? _blockUserCommand;

    public MvxAsyncCommand<UserViewInfo> BlockUserCommand =>
        _blockUserCommand ??= new(x => BlockUserAsync(x.Id));

    private async Task BlockUserAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        await _mediator.Send(new Application.Features.UserInformation.BlockRequest { Id = id });
        await SearchUsersAsync();
        TitleBarLocator.IsLoading = false;
    }

    #endregion

    #region UnblockUserCommand

    private MvxAsyncCommand<UserViewInfo>? _unblockUserCommand;
    public MvxAsyncCommand<UserViewInfo> UnblockUserCommand =>
        _unblockUserCommand ??= new(x => UnblockUserAsync(x.Id));

    private async Task UnblockUserAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        await _mediator.Send(new Application.Features.UserInformation.UnblockRequest { Id = id });
        await SearchUsersAsync();
        TitleBarLocator.IsLoading = false;
    }

    #endregion

    #region ApplySearchCommand

    private MvxAsyncCommand? _applySearchCommand;
    public MvxAsyncCommand ApplySearchCommand =>
        _applySearchCommand ??= new(SearchUsersAsync,
                                    () => !TitleBarLocator.IsLoading,
                                    true);

    #endregion

    #region UsersList

    private ObservableCollection<UserViewInfo>? _usersList;
    public ObservableCollection<UserViewInfo>? UsersList
    {
        get => _usersList;
        set => SetProperty(ref _usersList, value);
    }

    #endregion

    #region MovePaginationToBack

    private MvxAsyncCommand? _movePaginationToBack;
    public MvxAsyncCommand MovePaginationToBack =>
        _movePaginationToBack ??= new(() =>
                                      {
                                          FilterOptions.Pagination.CurrentPage--;

                                          return SearchUsersAsync();
                                      },
                                      () => !TitleBarLocator.IsLoading && FilterOptions.Pagination.IsMoveBackAvailable,
                                      true);

    #endregion

    #region MovePaginationToStraight

    private MvxAsyncCommand? _movePaginationToStraight;

    public MvxAsyncCommand MovePaginationToStraight =>
        _movePaginationToStraight ??= new(() =>
                                          {
                                              FilterOptions.Pagination.CurrentPage++;

                                              return SearchUsersAsync();
                                          },
                                          () => !TitleBarLocator.IsLoading && FilterOptions.Pagination.IsMoveStraightAvailable);

    #endregion

    #region FilterOptions

    private readonly FilterOptions _filterOptions = default!;
    public FilterOptions FilterOptions
    {
        get => _filterOptions;
        init => SetProperty(ref _filterOptions, value);
    }

    private void RaisePaginationCanExecute()
    {
        MovePaginationToBack.RaiseCanExecuteChanged();
        MovePaginationToStraight.RaiseCanExecuteChanged();
    }

    #endregion

    #region SelectedUser

    private UserViewInfo? _selectedUser;
    public UserViewInfo? SelectedUser
    {
        get => _selectedUser;
        set => SetProperty(ref _selectedUser, value);
    }

    #endregion

    #region OrderByList

    private ObservableCollection<string> _orderByList = default!;
    public ObservableCollection<string> OrderByList
    {
        get => _orderByList;
        set => SetProperty(ref _orderByList, value);
    }

    #endregion

    #region PageSizeList

    private ObservableCollection<int> _pageSizeList = default!;
    public ObservableCollection<int> PageSizeList
    {
        get => _pageSizeList;
        set => SetProperty(ref _pageSizeList, value);
    }

    #endregion

    private async Task SearchUsersAsync()
    {
        ChangeLoadingState(true);
        SelectedUser = null;

        try
        {
            var request = _mapper.Map<Application.Features.UserInformation.GetAllRequest>(FilterOptions);
            request.RoleIds = new()
                              {
                                  (int)RoleTypes.Manager,
                                  (int)RoleTypes.DefaultUser
                              };

            var users = await _notificationHost.SendRequestWithNotification(_mediator, request);
            _mapper.Map(users?.PagingInfo, FilterOptions.Pagination);
            UsersList = _mapper.Map<ObservableCollection<UserViewInfo>>(users?.Result);
        }
        finally
        {
            ChangeLoadingState(false);
        }

        RaisePaginationCanExecute();
        ApplySearchCommand.RaiseCanExecuteChanged();
    }
}