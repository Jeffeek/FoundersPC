using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Domain.Enums;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Services;
using MediatR;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using DeleteRequest = FoundersPC.Application.Features.Hardware.Base.DeleteRequest;
using RestoreRequest = FoundersPC.Application.Features.Hardware.Base.RestoreRequest;

namespace FoundersPC.UI.Admin.ViewModels;

public abstract class HardwareListBasePageViewModel<THardwareView,
                                                    THardwareInfo,
                                                    TGetAllRequest,
                                                    TGetRequest,
                                                    TDeleteRequest,
                                                    TRestoreRequest> : MvxViewModel
    where THardwareView : HardwareViewInfo
    where TGetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<THardwareView>>, new()
    where THardwareInfo : HardwareInfo, new()
    where TGetRequest : GetHardwareRequest, IRequest<THardwareInfo>, new()
    where TDeleteRequest : DeleteRequest, IRequest<Unit>, new()
    where TRestoreRequest : RestoreRequest, IRequest<THardwareInfo>, new()
{
    protected readonly IMediator Mediator;
    protected readonly IMapper Mapper;
    protected readonly SelectedObjectLocator SelectedObjectLocator;
    protected readonly HardwareType HardwareType;
    private readonly NotificationHost _notificationHost;
    protected readonly int DetailsPageId;
    public TitleBarLocator TitleBarLocator { get; }

    protected HardwareListBasePageViewModel(IMediator mediator,
                                            IMapper mapper,
                                            TitleBarLocator titleBarLocator,
                                            FilterOptions filterOptions,
                                            SelectedObjectLocator selectedObjectLocator,
                                            HardwareType hardwareType,
                                            NotificationHost notificationHost,
                                            int pageId,
                                            int detailsPageId)
    {
        Mediator = mediator;
        Mapper = mapper;
        TitleBarLocator = titleBarLocator;
        SelectedObjectLocator = selectedObjectLocator;
        HardwareType = hardwareType;
        _notificationHost = notificationHost;
        DetailsPageId = detailsPageId;
        FilterOptions = filterOptions;
        TitleBarLocator.IsLoadingChanged += _ => ApplySearchCommand.RaiseCanExecuteChanged();

        OrderByList = new()
                      {
                          "Id",
                          "Title",
                          "Created",
                          "Last Modified",
                          "Producer"
                      };

        PageSizeList = new()
                       {
                           10,
                           20,
                           50,
                           100
                       };

        TitleBarLocator.TabChanged += async tabId =>
                                      {
                                          if (tabId != pageId)
                                              return;

                                          await SearchHardwareAsync();
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

    private MvxCommand? _createHardwareCommand;

    public MvxCommand CreateHardwareCommand =>
        _createHardwareCommand ??= new(CreateNewHardwareAndMoveToDetails);

    private void CreateNewHardwareAndMoveToDetails()
    {
        SetSelectedHardware(new() { HardwareTypeId = (int)HardwareType});
        TitleBarLocator.CurrentFrameId = DetailsPageId;
    }

    #region MoveToHardwareDetailsPageCommand

    private MvxAsyncCommand<THardwareView>? _moveToDetailsPageCommand;

    public MvxAsyncCommand<THardwareView> MoveToHardwareDetailsPageCommand =>
        _moveToDetailsPageCommand ??= new(x => MoveToHardwareDetailsPageAsync(x.Id));

    private async Task MoveToHardwareDetailsPageAsync(int id)
    {
        TitleBarLocator.IsLoading = true;

        var hardwareInfo = await _notificationHost.SendRequestWithNotification(Mediator, new TGetRequest { Id = id, HardwareTypeId = (int)HardwareType });

        if (hardwareInfo == null)
        {
            _notificationHost.ShowWarningNotification("Something bad happened with your request..");

            return;
        }

        SetSelectedHardware(hardwareInfo);
        TitleBarLocator.CurrentFrameId = DetailsPageId;
        TitleBarLocator.IsLoading = false;
    }

    #endregion

    #region DeleteHardwareCommand

    private MvxAsyncCommand<THardwareView>? _deleteHardwareCommand;

    public MvxAsyncCommand<THardwareView> DeleteHardwareCommand =>
        _deleteHardwareCommand ??= new(x => DeleteHardwareAsync(x.Id));

    private async Task DeleteHardwareAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        await _notificationHost.SendRequestWithNotification(Mediator, new TDeleteRequest { HardwareTypeId = (int)HardwareType, Id = id });
        _notificationHost.ShowWarningNotification($"Hardware with Id {id} is deleted");
        await SearchHardwareAsync();
        TitleBarLocator.IsLoading = false;
    }

    #endregion

    #region RestoreHardwareCommand

    private MvxAsyncCommand<THardwareView>? _restoreHardwareCommand;

    public MvxAsyncCommand<THardwareView> RestoreHardwareCommand =>
        _restoreHardwareCommand ??= new(x => RestoreHardwareAsync(x.Id));

    private async Task RestoreHardwareAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        await _notificationHost.SendRequestWithNotification(Mediator, new TRestoreRequest { HardwareTypeId = (int)HardwareType, Id = id });
        _notificationHost.ShowDoneNotification($"Hardware with Id {id} is restored");
        await SearchHardwareAsync();
        TitleBarLocator.IsLoading = false;
    }

    #endregion

    #region ApplySearchCommand

    private MvxAsyncCommand? _applySearchCommand;

    public MvxAsyncCommand ApplySearchCommand =>
        _applySearchCommand ??= new(SearchHardwareAsync,
                                    () => !TitleBarLocator.IsLoading,
                                    true);

    #endregion

    #region HardwareList

    private ObservableCollection<THardwareView>? _hardwareList;

    public ObservableCollection<THardwareView>? HardwareList
    {
        get => _hardwareList;
        set => SetProperty(ref _hardwareList, value);
    }

    #endregion

    #region MovePaginationToBack

    private MvxAsyncCommand? _movePaginationToBack;

    public MvxAsyncCommand MovePaginationToBack =>
        _movePaginationToBack ??= new(() =>
                                      {
                                          FilterOptions.Pagination.CurrentPage--;

                                          return SearchHardwareAsync();
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

                                              return SearchHardwareAsync();
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

    #region SelectedHardware

    private THardwareView? _selectedHardware;

    public THardwareView? SelectedHardware
    {
        get => _selectedHardware;
        set => SetProperty(ref _selectedHardware, value);
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

    private async Task SearchHardwareAsync()
    {
        ChangeLoadingState(true);
        SelectedHardware = null;

        try
        {
            var hardware = await _notificationHost.SendRequestWithNotification(Mediator, Mapper.Map<TGetAllRequest>(FilterOptions));
            Mapper.Map(hardware?.PagingInfo, FilterOptions.Pagination);
            HardwareList = new(hardware?.Result ?? new List<THardwareView>());
        }
        finally
        {
            ChangeLoadingState(false);
        }

        RaisePaginationCanExecute();
        ApplySearchCommand.RaiseCanExecuteChanged();
    }

    protected abstract void SetSelectedHardware(THardwareInfo hardware);
}