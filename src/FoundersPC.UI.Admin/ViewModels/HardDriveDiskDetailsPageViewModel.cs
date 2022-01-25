﻿using AutoMapper;
using FoundersPC.Application.Features.Hardware.HardDriveDisk;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class HardDriveDiskDetailsPageViewModel : HardwareDetailsPageViewModel<HardDriveDiskInfo, HardDriveDiskInfoViewModel, CreateRequest, UpdateRequest>
{
    public HardDriveDiskDetailsPageViewModel(IMediator mediator,
                                             IMapper mapper,
                                             SelectedObjectLocator selectedObjectLocator,
                                             MetadataPackageLocator metadataPackageLocator,
                                             TitleBarLocator titleBarLocator)
        : base(mediator,
               mapper,
               selectedObjectLocator,
               metadataPackageLocator,
               titleBarLocator,
               TitleBarConstants.HardDriveDiskDetailsPageId,
               TitleBarConstants.HardDriveDisksPageId) { }

    protected override void SubscribeToHardwareLocator() =>
        SelectedObjectLocator.SelectedHardDriveDiskChanged += OnSelectedHardwareChanged;
}