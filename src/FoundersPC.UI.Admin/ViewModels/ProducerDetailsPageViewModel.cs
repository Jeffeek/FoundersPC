using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class ProducerDetailsPageViewModel : BindableBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly TitleBarLocator _titleBarLocator;

    public MetadataPackageLocator MetadataPackageLocator { get; }

    public ProducerDetailsPageViewModel(IMediator mediator,
                                        IMapper mapper,
                                        SelectedObjectLocator selectedObjectLocator,
                                        MetadataPackageLocator metadataPackageLocator,
                                        TitleBarLocator titleBarLocator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _titleBarLocator = titleBarLocator;
        MetadataPackageLocator = metadataPackageLocator;
        selectedObjectLocator.SelectedProducerChanged += OnSelectedProducerChanged;
        RefreshLocator.SaveRefresh += () => SaveCommand.RaiseCanExecuteChanged();
    }

    private void OnSelectedProducerChanged(ProducerInfo? obj)
    {
        if (obj == null)
            EditableProducer = null;

        EditableProducer = _mapper.Map<ProducerInfoViewModel>(obj);
    }

    #region IsUpdating

    private bool _isUpdating;
    public bool IsUpdating
    {
        get => _isUpdating;
        set
        {
            SetProperty(ref _isUpdating, value);
            SaveCommand.RaiseCanExecuteChanged();
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

    #region EditableProducer

    private ProducerInfoViewModel? _editableProducer;

    public ProducerInfoViewModel? EditableProducer
    {
        get => _editableProducer;
        set
        {
            SetProperty(ref _editableProducer, value);
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    #endregion

    #region SaveCommand

    private MvxAsyncCommand? _saveCommand;

    public MvxAsyncCommand SaveCommand =>
        _saveCommand ??= new(async () =>
                             {
                                 ChangeLoadingState(true);

                                 try
                                 {
                                     await InsertOrUpdateProducerAsync();
                                     GoBack();
                                 }
                                 finally
                                 {
                                     ChangeLoadingState(false);
                                 }
                             },
                             CanInsertOrUpdateProducer,
                             true);

    protected virtual bool CanInsertOrUpdateProducer() =>
        EditableProducer != null
        && !IsUpdating
        && !String.IsNullOrEmpty(EditableProducer.FullName)
        && !String.IsNullOrWhiteSpace(EditableProducer.FullName);

    private Task InsertOrUpdateProducerAsync()
    {
        var request = GetInsertOrUpdateRequest();

        return request == null ? Task.CompletedTask : _mediator.Send(request);
    }

    private IRequest<ProducerInfo>? GetInsertOrUpdateRequest()
    {
        if (EditableProducer == null)
            return default;

        if (EditableProducer.Id == 0)
            return _mapper.Map<Application.Features.Producer.CreateRequest>(EditableProducer);

        return _mapper.Map<Application.Features.Producer.UpdateRequest>(EditableProducer);
    }

    #endregion

    #region GoBackCommand

    private MvxCommand? _goBackCommand;

    public MvxCommand GoBackCommand =>
        _goBackCommand ??= new(GoBack,
                               () => !IsUpdating);

    private void GoBack()
    {
        EditableProducer = null;
        _titleBarLocator.CurrentFrameId = TitleBarConstants.ProducersPageId;
    }

    #endregion
}