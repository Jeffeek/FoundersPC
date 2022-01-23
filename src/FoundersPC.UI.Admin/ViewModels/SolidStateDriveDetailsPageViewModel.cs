using AutoMapper;
using FoundersPC.Application.Features.Hardware.SolidStateDrive;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class SolidStateDriveDetailsPageViewModel : HardwareDetailsPageViewModel<SolidStateDriveInfo, SolidStateDriveInfoViewModel, CreateRequest, UpdateRequest>
{
    public SolidStateDriveDetailsPageViewModel(IMediator mediator,
                                               IMapper mapper,
                                               SelectedObjectLocator selectedObjectLocator,
                                               MetadataPackageLocator metadataPackageLocator,
                                               TitleBarLocator titleBarLocator)
        : base(mediator,
               mapper,
               selectedObjectLocator,
               metadataPackageLocator,
               titleBarLocator,
               TitleBarConstants.SolidStateDriveDetailsPageId,
               TitleBarConstants.SolidStateDrivesPageId) { }

    protected override void SubscribeToHardwareLocator() => SelectedObjectLocator.SelectedSolidStateDriveChanged += OnSelectedHardwareChanged;
}