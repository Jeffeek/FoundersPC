using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace FoundersPC.UI.Admin.ViewModels;

public abstract class HardwareDetailsPageViewModel<THardwareInfo,
                                                   THardwareInfoViewModel,
                                                   TCreateRequest,
                                                   TUpdateRequest> : MvxViewModel
    where THardwareInfo : HardwareInfo
    where THardwareInfoViewModel : HardwareInfoViewModel
    where TCreateRequest : IRequest<THardwareInfo>
    where TUpdateRequest : IRequest<THardwareInfo>
{
    protected readonly IMediator Mediator;
    protected readonly IMapper Mapper;
    protected readonly SelectedObjectLocator SelectedObjectLocator;
    private readonly TitleBarLocator _titleBarLocator;
    private readonly int _goBackId;

    public MetadataPackageLocator MetadataPackageLocator { get; }

    protected HardwareDetailsPageViewModel(IMediator mediator,
                                           IMapper mapper,
                                           SelectedObjectLocator selectedObjectLocator,
                                           MetadataPackageLocator metadataPackageLocator,
                                           TitleBarLocator titleBarLocator,
                                           int thisPageId,
                                           int goBackId)
    {
        Mediator = mediator;
        Mapper = mapper;
        SelectedObjectLocator = selectedObjectLocator;
        _titleBarLocator = titleBarLocator;
        _goBackId = goBackId;
        MetadataPackageLocator = metadataPackageLocator;
        SubscribeToHardwareLocator();

        _titleBarLocator.TabChanged += async pageId =>
                                       {
                                           if (pageId != thisPageId)
                                               return;

                                           await OnPageOpenedHandler();
                                       };

        RefreshLocator.SaveRefresh += () => SaveCommand.RaiseCanExecuteChanged();
    }

    protected abstract void SubscribeToHardwareLocator();

    protected virtual Task OnPageOpenedHandler() => Task.CompletedTask;

    protected void OnSelectedHardwareChanged(THardwareInfo? obj)
    {
        if (obj == null)
            EditableHardware = null;

        EditableHardware = Mapper.Map<THardwareInfoViewModel>(obj);
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

    #region EditableHardware

    private THardwareInfoViewModel? _editableHardware;

    public THardwareInfoViewModel? EditableHardware
    {
        get => _editableHardware;
        set
        {
            SetProperty(ref _editableHardware, value);
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
                                     var hardware = await InsertOrUpdateHardwareAsync();
                                     if (hardware != null)
                                         OnInsertOrUpdate(hardware.Id);
                                     GoBack();
                                 }
                                 finally
                                 {
                                     ChangeLoadingState(false);
                                 }
                             },
                             CanInsertOrUpdateHardware,
                             true);

    protected bool CanInsertOrUpdateHardware() =>
        EditableHardware != null
        && !IsUpdating
        && !String.IsNullOrEmpty(EditableHardware.Title)
        && !String.IsNullOrWhiteSpace(EditableHardware.Title)
        && EditableHardware.ProducerId > 0;

    protected virtual void OnInsertOrUpdate(int id) { }

    private Task<THardwareInfo> InsertOrUpdateHardwareAsync()
    {
        var request = GetInsertOrUpdateRequest();

        return request == null ? Task.FromResult((THardwareInfo?)null)! : Mediator.Send(request);
    }

    private IRequest<THardwareInfo>? GetInsertOrUpdateRequest()
    {
        if (EditableHardware == null)
            return default;

        if (EditableHardware.Id == 0)
            return Mapper.Map<TCreateRequest>(EditableHardware);

        return Mapper.Map<TUpdateRequest>(EditableHardware);
    }

    #endregion

    #region GoBackCommand

    private MvxCommand? _goBackCommand;

    public MvxCommand GoBackCommand =>
        _goBackCommand ??= new(GoBack,
                               () => !IsUpdating);

    private void GoBack()
    {
        EditableHardware = null;
        _titleBarLocator.CurrentFrameId = _goBackId;
    }

    #endregion
}