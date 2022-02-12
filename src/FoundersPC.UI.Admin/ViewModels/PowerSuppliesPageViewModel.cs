using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.PowerSupply;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.UI.Admin.Locators;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class PowerSuppliesPageViewModel : HardwareListBasePageViewModel<PowerSupplyViewInfo, PowerSupplyInfo, GetAllRequest, GetRequest, DeleteRequest, RestoreRequest>
{
    public PowerSuppliesPageViewModel(IMediator mediator,
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
               Domain.Enums.HardwareType.PowerSupply,
               notificationHost,
               TitleBarConstants.PowerSuppliesPageId,
               TitleBarConstants.PowerSupplyDetailsPageId) { }

    protected override void SetSelectedHardware(PowerSupplyInfo hardware) =>
        SelectedObjectLocator.SelectedPowerSupply = hardware;
}