using AutoMapper;
using FoundersPC.Application.Features.Hardware.SolidStateDrive;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.UI.Admin.Locators;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class SolidStateDrivesPageViewModel : HardwareListBasePageViewModel<SolidStateDriveViewInfo, SolidStateDriveInfo, GetAllRequest, GetRequest, DeleteRequest, RestoreRequest>
{
    public SolidStateDrivesPageViewModel(IMediator mediator,
                                         IMapper mapper,
                                         TitleBarLocator titleBarLocator,
                                         SelectedObjectLocator selectedObjectLocator,
                                         FilterOptions pagination)
        : base(mediator,
               mapper,
               titleBarLocator,
               pagination,
               selectedObjectLocator,
               Domain.Enums.HardwareType.SSD,
               TitleBarConstants.SolidStateDrivesPageId,
               TitleBarConstants.SolidStateDriveDetailsPageId) { }

    protected override void SetSelectedHardware(SolidStateDriveInfo hardware) =>
        SelectedObjectLocator.SelectedSolidStateDrive = hardware;
}