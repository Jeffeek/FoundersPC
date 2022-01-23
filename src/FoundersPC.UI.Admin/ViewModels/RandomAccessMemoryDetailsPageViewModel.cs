﻿using AutoMapper;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class RandomAccessMemoryDetailsPageViewModel : HardwareDetailsPageViewModel<RandomAccessMemoryInfo, RandomAccessMemoryInfoViewModel, CreateRequest, UpdateRequest>
{
    public RandomAccessMemoryDetailsPageViewModel(IMediator mediator,
                                                  IMapper mapper,
                                                  SelectedObjectLocator selectedObjectLocator,
                                                  MetadataPackageLocator metadataPackageLocator,
                                                  TitleBarLocator titleBarLocator)
        : base(mediator,
               mapper,
               selectedObjectLocator,
               metadataPackageLocator,
               titleBarLocator,
               TitleBarConstants.RandomAccessMemoryDetailsPageId,
               TitleBarConstants.RandomAccessMemoriesPageId) { }

    protected override void SubscribeToHardwareLocator() =>
        SelectedObjectLocator.SelectedRandomAccessMemoryChanged += OnSelectedHardwareChanged;
}