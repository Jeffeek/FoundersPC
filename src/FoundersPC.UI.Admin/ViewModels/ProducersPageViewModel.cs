using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Services;
using MediatR;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class ProducersPageViewModel : BindableBase
{
    protected readonly IMediator Mediator;
    protected readonly IMapper Mapper;
    protected readonly SelectedObjectLocator SelectedObjectLocator;
    private readonly NotificationHost _notificationHost;
    private readonly MetadataPackageLocator _metadataPackageLocator;
    protected readonly int DetailsPageId;
    public TitleBarLocator TitleBarLocator { get; }

    public ProducersPageViewModel(IMediator mediator,
                                  IMapper mapper,
                                  FilterOptions filterOptions,
                                  SelectedObjectLocator selectedObjectLocator,
                                  TitleBarLocator titleBarLocator,
                                  NotificationHost notificationHost,
                                  MetadataPackageLocator metadataPackageLocator)
    {
        TitleBarLocator = titleBarLocator;
        Mediator = mediator;
        Mapper = mapper;
        SelectedObjectLocator = selectedObjectLocator;
        _notificationHost = notificationHost;
        _metadataPackageLocator = metadataPackageLocator;
        DetailsPageId = TitleBarConstants.ProducerDetailsPageId;
        FilterOptions = filterOptions;
        TitleBarLocator.IsLoadingChanged += _ => ApplySearchCommand.RaiseCanExecuteChanged();

        OrderByList = new()
                      {
                          "Id",
                          "FullName",
                          "Created",
                          "Last Modified"
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
                                          if (tabId != TitleBarConstants.ProducersPageId)
                                              return;

                                          await SearchProducersAsync();
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

    #region CreateProducerCommand

    private MvxCommand? _createProducerCommand;
    public MvxCommand CreateProducerCommand =>
        _createProducerCommand ??= new(CreateNewProducerAndMoveToDetails);

    private void CreateNewProducerAndMoveToDetails()
    {
        SelectedObjectLocator.SelectedProducer = new();
        TitleBarLocator.CurrentFrameId = DetailsPageId;
    }

    #endregion

    #region MoveToHardwareDetailsPageCommand

    private MvxAsyncCommand<ProducerViewInfo>? _moveToProducerDetailsPageCommand;
    public MvxAsyncCommand<ProducerViewInfo> MoveToProducerDetailsPageCommand =>
        _moveToProducerDetailsPageCommand ??= new(x => MoveToProducerDetailsPageAsync(x.Id));

    private async Task MoveToProducerDetailsPageAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        var hardwareInfo = await Mediator.Send(new Application.Features.Producer.GetRequest { Id = id });
        SelectedObjectLocator.SelectedProducer = hardwareInfo;
        TitleBarLocator.CurrentFrameId = DetailsPageId;
        TitleBarLocator.IsLoading = false;
    }

    #endregion

    #region DeleteProducerCommand

    private MvxAsyncCommand<ProducerViewInfo>? _deleteProducerCommand;

    public MvxAsyncCommand<ProducerViewInfo> DeleteProducerCommand =>
        _deleteProducerCommand ??= new(x => DeleteProducerAsync(x.Id));

    private async Task DeleteProducerAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        await _notificationHost.SendRequestWithNotification(Mediator, new Application.Features.Producer.DeleteRequest { Id = id });
        _notificationHost.ShowWarningNotification($"Producer with Id {id} is deleted");
        await SearchProducersAsync();
        TitleBarLocator.IsLoading = false;

        var producerFromPackage = _metadataPackageLocator.MetadataPackage.Producers.FirstOrDefault(x => x.Id == id);
        if (producerFromPackage != null)
            _metadataPackageLocator.MetadataPackage.Producers.Remove(producerFromPackage);
    }

    #endregion

    #region RestoreProducerCommand

    private MvxAsyncCommand<ProducerViewInfo>? _restoreProducerCommand;
    public MvxAsyncCommand<ProducerViewInfo> RestoreProducerCommand =>
        _restoreProducerCommand ??= new(x => RestoreProducerAsync(x.Id));

    private async Task RestoreProducerAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        await _notificationHost.SendRequestWithNotification(Mediator, new Application.Features.Producer.RestoreRequest { Id = id });
        _notificationHost.ShowDoneNotification($"Producer with Id {id} is restored");
        await SearchProducersAsync();
        TitleBarLocator.IsLoading = false;

        if (SelectedProducer != null)
            _metadataPackageLocator.MetadataPackage.Producers.Add(new()
                                                                  {
                                                                      Id = id,
                                                                      Value = SelectedProducer.FullName
                                                                  });
    }

    #endregion

    #region ApplySearchCommand

    private MvxAsyncCommand? _applySearchCommand;
    public MvxAsyncCommand ApplySearchCommand =>
        _applySearchCommand ??= new(SearchProducersAsync,
                                    () => !TitleBarLocator.IsLoading,
                                    true);

    #endregion

    #region HardwareList

    private ObservableCollection<ProducerViewInfo>? _producersList;
    public ObservableCollection<ProducerViewInfo>? ProducersList
    {
        get => _producersList;
        set => SetProperty(ref _producersList, value);
    }

    private ProducerViewInfo? _selectedProducer;
    public ProducerViewInfo? SelectedProducer
    {
        get => _selectedProducer;
        set => SetProperty(ref _selectedProducer, value);
    }

    #endregion

    #region MovePaginationToBack

    private MvxAsyncCommand? _movePaginationToBack;

    public MvxAsyncCommand MovePaginationToBack =>
        _movePaginationToBack ??= new(() =>
                                      {
                                          FilterOptions.Pagination.CurrentPage--;

                                          return SearchProducersAsync();
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

                                              return SearchProducersAsync();
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

    private async Task SearchProducersAsync()
    {
        ChangeLoadingState(true);

        try
        {
            var producers = await _notificationHost.SendRequestWithNotification(Mediator, Mapper.Map<Application.Features.Producer.GetAllRequest>(FilterOptions));
            Mapper.Map(producers?.PagingInfo, FilterOptions.Pagination);
            ProducersList = new(producers?.Result ?? new List<ProducerViewInfo>());
        }
        finally
        {
            ChangeLoadingState(false);
        }

        RaisePaginationCanExecute();
        ApplySearchCommand.RaiseCanExecuteChanged();
    }
}