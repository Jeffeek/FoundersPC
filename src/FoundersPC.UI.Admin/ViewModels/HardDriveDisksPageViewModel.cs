using AutoMapper;
using FoundersPC.Application.Features.Hardware.HardDriveDisk;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.UI.Admin.Locators;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class HardDriveDisksPageViewModel : HardwareListBasePageViewModel<HardDriveDiskViewInfo, HardDriveDiskInfo, GetAllRequest, GetRequest, DeleteRequest, RestoreRequest>
{
    public HardDriveDisksPageViewModel(IMediator mediator,
                                       IMapper mapper,
                                       TitleBarLocator titleBarLocator,
                                       SelectedObjectLocator selectedObjectLocator,
                                       FilterOptions pagination)
        : base(mediator,
               mapper,
               titleBarLocator,
               pagination,
               selectedObjectLocator,
               Domain.Enums.HardwareType.HDD,
               TitleBarConstants.HardDriveDisksPageId,
               TitleBarConstants.HardDriveDiskDetailsPageId) { }

    protected override void SetSelectedHardware(HardDriveDiskInfo hardware) =>
        SelectedObjectLocator.SelectedHardDriveDisk = hardware;
}