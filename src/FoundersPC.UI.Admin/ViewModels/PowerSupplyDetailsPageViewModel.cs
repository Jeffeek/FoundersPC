﻿using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.PowerSupply;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class PowerSupplyDetailsPageViewModel : HardwareDetailsPageViewModel<PowerSupplyInfo, PowerSupplyInfoViewModel, CreateRequest, UpdateRequest>
{
    public PowerSupplyDetailsPageViewModel(IMediator mediator,
                                           IMapper mapper,
                                           SelectedObjectLocator selectedObjectLocator,
                                           MetadataPackageLocator metadataPackageLocator,
                                           TitleBarLocator titleBarLocator,
                                           NotificationHost notificationHost)
        : base(mediator,
               mapper,
               selectedObjectLocator,
               metadataPackageLocator,
               titleBarLocator,
               notificationHost,
               TitleBarConstants.PowerSupplyDetailsPageId,
               TitleBarConstants.PowerSuppliesPageId) { }

    protected override void SubscribeToHardwareLocator() =>
        SelectedObjectLocator.SelectedPowerSupplyChanged += OnSelectedHardwareChanged;
}