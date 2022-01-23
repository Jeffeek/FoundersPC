using AutoMapper;
using FoundersPC.Application.Features.Hardware.Motherboard;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.UI.Admin.Locators;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class MotherboardsPageViewModel : HardwareListBasePageViewModel<MotherboardViewInfo, MotherboardInfo, GetAllRequest, GetRequest, DeleteRequest, RestoreRequest>
{
    public MotherboardsPageViewModel(IMediator mediator,
                                     IMapper mapper,
                                     TitleBarLocator titleBarLocator,
                                     SelectedObjectLocator selectedObjectLocator,
                                     FilterOptions pagination)
        : base(mediator,
               mapper,
               titleBarLocator,
               pagination,
               selectedObjectLocator,
               Domain.Enums.HardwareType.MB,
               TitleBarConstants.MotherboardsPageId,
               TitleBarConstants.MotherboardDetailsPageId) { }

    protected override void SetSelectedHardware(MotherboardInfo hardware) =>
        SelectedObjectLocator.SelectedMotherboard = hardware;
}