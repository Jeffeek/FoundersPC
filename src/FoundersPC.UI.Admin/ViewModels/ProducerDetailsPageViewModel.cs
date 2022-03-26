using System;
using System.Threading.Tasks;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using FoundersPC.UI.Admin.Services;
using MediatR;
using MvvmCross.Commands;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class ProducerDetailsPageViewModel : BindableBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly TitleBarLocator _titleBarLocator;
    private readonly NotificationHost _notificationHost;

    public MetadataPackageLocator MetadataPackageLocator { get; }

    public ProducerDetailsPageViewModel(IMediator mediator,
                                        IMapper mapper,
                                        SelectedObjectLocator selectedObjectLocator,
                                        MetadataPackageLocator metadataPackageLocator,
                                        TitleBarLocator titleBarLocator,
                                        NotificationHost notificationHost)
    {
        _mediator = mediator;
        _mapper = mapper;
        _titleBarLocator = titleBarLocator;
        _notificationHost = notificationHost;
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
                                     var producer = await InsertOrUpdateProducerAsync();

                                     if (producer == null)
                                     {
                                         _notificationHost.DisplayAsync(new SimpleNotification("Not found", SimpleNotificationType.Error), Position.BottomLeft);
                                         GoBack();
                                         return;
                                     }

                                     if (EditableProducer?.Id == 0)
                                         MetadataPackageLocator.MetadataPackage.Producers.Add(new()
                                                                                              {
                                                                                                  Id = producer.Id,
                                                                                                  Value = producer.FullName
                                                                                              });

                                     _notificationHost.ShowDoneNotification($"Producer {producer.FullName} {(EditableProducer?.Id == 0 ? "created" : "updated")} successfully");
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

    private Task<ProducerInfo?> InsertOrUpdateProducerAsync()
    {
        var request = GetInsertOrUpdateRequest();

        return request == null ? Task.FromResult((ProducerInfo?)null)! : _notificationHost.SendRequestWithNotification(_mediator, request);
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