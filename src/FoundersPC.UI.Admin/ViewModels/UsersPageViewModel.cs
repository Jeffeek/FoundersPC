using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class UsersPageViewModel : BindableBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly TitleBarLocator _titleBarLocator;
    private readonly SelectedObjectLocator _selectedObjectLocator;

    public UsersPageViewModel(IMediator mediator,
                              IMapper mapper,
                              TitleBarLocator titleBarLocator,
                              SelectedObjectLocator selectedObjectLocator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _titleBarLocator = titleBarLocator;
        _selectedObjectLocator = selectedObjectLocator;

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
        if (state == _titleBarLocator.IsLoading)
            return;

        if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            _titleBarLocator.IsLoading = state;
        else
            System.Windows.Application.Current.Dispatcher.Invoke(() => _titleBarLocator.IsLoading = state);
    }

    private ObservableCollection<UserViewModel> _users = new();
    public ObservableCollection<UserViewModel> Users
    {
        get => _users;
        set => SetProperty(ref _users, value);
    }

    private MvxCommand? _createUserCommand;
    public MvxCommand CreateUserCommand =>
        _createUserCommand ??= new(CreateNewUserAndMoveToDetails);

    private void CreateNewUserAndMoveToDetails()
    {
        _selectedObjectLocator.SelectedProducer = new();
        _titleBarLocator.CurrentFrameId = TitleBarConstants.UserDetailsPageId;
    }

    #region MoveToUserDetailsPageCommand

    private MvxAsyncCommand<ProducerViewInfo>? _moveToUserDetailsPageCommand;
    public MvxAsyncCommand<ProducerViewInfo> MoveToUserDetailsPageCommand =>
        _moveToUserDetailsPageCommand ??= new(x => MoveToUserDetailsPageAsync(x.Id));

    private async Task MoveToUserDetailsPageAsync(int id)
    {
        _titleBarLocator.IsLoading = true;
        var userInfo = await _mediator.Send(new Application.Features.UserInformation.GetRequest { Id = id });
        _selectedObjectLocator.SelectedUser = userInfo;
        _titleBarLocator.CurrentFrameId = TitleBarConstants.UserDetailsPageId;
        _titleBarLocator.IsLoading = false;
    }

    #endregion

    #region BlockUserCommand

    private MvxAsyncCommand<ProducerViewInfo>? _blockUserCommand;

    public MvxAsyncCommand<ProducerViewInfo> BlockUserCommand =>
        _blockUserCommand ??= new(x => BlockUserAsync(x.Id));

    private async Task BlockUserAsync(int id)
    {
        _titleBarLocator.IsLoading = true;
        await _mediator.Send(new Application.Features.UserInformation.BlockRequest { Id = id });
        await SearchUsersAsync();
        _titleBarLocator.IsLoading = false;
    }

    #endregion

    #region UnblockUserCommand

    private MvxAsyncCommand<ProducerViewInfo>? _unblockUserCommand;
    public MvxAsyncCommand<ProducerViewInfo> UnblockUserCommand =>
        _unblockUserCommand ??= new(x => UnblockUserAsync(x.Id));

    private async Task UnblockUserAsync(int id)
    {
        _titleBarLocator.IsLoading = true;
        await _mediator.Send(new Application.Features.UserInformation.UnblockRequest { Id = id });
        await SearchUsersAsync();
        _titleBarLocator.IsLoading = false;
    }

    #endregion

    #region ApplySearchCommand

    private MvxAsyncCommand? _applySearchCommand;
    public MvxAsyncCommand ApplySearchCommand =>
        _applySearchCommand ??= new(SearchUsersAsync,
                                    () => !_titleBarLocator.IsLoading,
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
                                      () => !_titleBarLocator.IsLoading && FilterOptions.Pagination.IsMoveBackAvailable,
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
                                          () => !_titleBarLocator.IsLoading && FilterOptions.Pagination.IsMoveStraightAvailable);

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

    private UserViewModel? _selectedUser;

    public UserViewModel? SelectedUser
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
            var hardware = await _mediator.Send(_mapper.Map<Application.Features.UserInformation.GetAllRequest>(FilterOptions));
            _mapper.Map(hardware.PagingInfo, FilterOptions.Pagination);
            Users = new(_mapper.Map<IEnumerable<UserViewModel>>(hardware.Result));
        }
        finally
        {
            ChangeLoadingState(false);
        }

        RaisePaginationCanExecute();
        ApplySearchCommand.RaiseCanExecuteChanged();
    }
}