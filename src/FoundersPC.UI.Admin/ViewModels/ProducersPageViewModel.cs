using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class ProducersPageViewModel : BindableBase
{
    protected readonly IMediator Mediator;
    protected readonly IMapper Mapper;
    protected readonly SelectedObjectLocator SelectedObjectLocator;
    protected readonly int DetailsPageId;
    public TitleBarLocator TitleBarLocator { get; }

    public ProducersPageViewModel(IMediator mediator,
                                  IMapper mapper,
                                  SelectedObjectLocator selectedObjectLocator,
                                  TitleBarLocator titleBarLocator)
    {
        TitleBarLocator = titleBarLocator;
        Mediator = mediator;
        Mapper = mapper;
        SelectedObjectLocator = selectedObjectLocator;
        DetailsPageId = TitleBarConstants.ProducerDetailsPageId;
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

    private MvxCommand? _createProducerCommand;
    public MvxCommand CreateProducerCommand =>
        _createProducerCommand ??= new(CreateNewProducerAndMoveToDetails);

    private void CreateNewProducerAndMoveToDetails()
    {
        SelectedObjectLocator.SelectedProducer = new();
        TitleBarLocator.CurrentFrameId = DetailsPageId;
    }

    #region MoveToHardwareDetailsPageCommand

    private MvxAsyncCommand<ProducerViewInfo>? _moveToDetailsPageCommand;
    public MvxAsyncCommand<ProducerViewInfo> MoveToHardwareDetailsPageCommand =>
        _moveToDetailsPageCommand ??= new(x => MoveToProducerDetailsPageAsync(x.Id));

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

    private MvxAsyncCommand<ProducerViewInfo>? _deleteHardwareCommand;

    public MvxAsyncCommand<ProducerViewInfo> DeleteHardwareCommand =>
        _deleteHardwareCommand ??= new(x => DeleteHardwareAsync(x.Id));

    private async Task DeleteHardwareAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        await Mediator.Send(new Application.Features.Producer.DeleteRequest { Id = id });
        await SearchProducersAsync();
        TitleBarLocator.IsLoading = false;
    }

    #endregion

    #region RestoreProducerCommand

    private MvxAsyncCommand<ProducerViewInfo>? _restoreHardwareCommand;
    public MvxAsyncCommand<ProducerViewInfo> RestoreHardwareCommand =>
        _restoreHardwareCommand ??= new(x => RestoreProducerAsync(x.Id));

    private async Task RestoreProducerAsync(int id)
    {
        TitleBarLocator.IsLoading = true;
        await Mediator.Send(new Application.Features.Producer.RestoreRequest { Id = id });
        await SearchProducersAsync();
        TitleBarLocator.IsLoading = false;
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

    #region SelectedProducer

    private ProducerInfoViewModel? _selectedProducer;

    public ProducerInfoViewModel? SelectedProducer
    {
        get => _selectedProducer;
        set => SetProperty(ref _selectedProducer, value);
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
        SelectedProducer = null;

        try
        {
            var hardware = await Mediator.Send(Mapper.Map<Application.Features.Producer.GetAllRequest>(FilterOptions));
            Mapper.Map(hardware.PagingInfo, FilterOptions.Pagination);
            ProducersList = new(hardware.Result);
        }
        finally
        {
            ChangeLoadingState(false);
        }

        RaisePaginationCanExecute();
        ApplySearchCommand.RaiseCanExecuteChanged();
    }
}