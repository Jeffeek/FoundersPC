using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.Processor;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.UI.Admin.Locators;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class ProcessorsPageViewModel : HardwareListBasePageViewModel<ProcessorViewInfo, ProcessorInfo, GetAllRequest, GetRequest, DeleteRequest, RestoreRequest>
{
    public ProcessorsPageViewModel(IMediator mediator,
                                   IMapper mapper,
                                   TitleBarLocator titleBarLocator,
                                   SelectedObjectLocator selectedObjectLocator,
                                   FilterOptions pagination,
                                   NotificationHost notificationHost)
        : base(mediator,
               mapper,
               titleBarLocator,
               pagination,
               selectedObjectLocator,
               Domain.Enums.HardwareType.CPU,
               notificationHost,
               TitleBarConstants.ProcessorsPageId,
               TitleBarConstants.ProcessorDetailsPageId) { }

    protected override void SetSelectedHardware(ProcessorInfo hardware) =>
        SelectedObjectLocator.SelectedProcessor = hardware;
}