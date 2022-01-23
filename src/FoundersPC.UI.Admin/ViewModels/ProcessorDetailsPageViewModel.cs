using AutoMapper;
using FoundersPC.Application.Features.Hardware.Processor;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class ProcessorDetailsPageViewModel : HardwareDetailsPageViewModel<ProcessorInfo, ProcessorInfoViewModel, CreateRequest, UpdateRequest>
{
    public ProcessorDetailsPageViewModel(IMediator mediator,
                                         IMapper mapper,
                                         SelectedObjectLocator selectedObjectLocator,
                                         MetadataPackageLocator metadataPackageLocator,
                                         TitleBarLocator titleBarLocator)
        : base(mediator,
               mapper,
               selectedObjectLocator,
               metadataPackageLocator,
               titleBarLocator,
               TitleBarConstants.ProcessorDetailsPageId,
               TitleBarConstants.ProcessorsPageId) { }

    protected override void SubscribeToHardwareLocator() =>
        SelectedObjectLocator.SelectedProcessorChanged += OnSelectedHardwareChanged;
}