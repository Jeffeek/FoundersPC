using AutoMapper;
using FoundersPC.Application.Features.Hardware.Motherboard;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class MotherboardDetailsPageViewModel : HardwareDetailsPageViewModel<MotherboardInfo, MotherboardInfoViewModel, CreateRequest, UpdateRequest>
{
    public MotherboardDetailsPageViewModel(IMediator mediator,
                                           IMapper mapper,
                                           SelectedObjectLocator selectedObjectLocator,
                                           MetadataPackageLocator metadataPackageLocator,
                                           TitleBarLocator titleBarLocator)
        : base(mediator,
               mapper,
               selectedObjectLocator,
               metadataPackageLocator,
               titleBarLocator,
               TitleBarConstants.MotherboardDetailsPageId,
               TitleBarConstants.MotherboardsPageId) { }

    protected override void SubscribeToHardwareLocator() =>
        SelectedObjectLocator.SelectedMotherboardChanged += OnSelectedHardwareChanged;
}