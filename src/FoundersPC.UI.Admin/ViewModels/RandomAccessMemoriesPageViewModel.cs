using AutoMapper;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.UI.Admin.Locators;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class RandomAccessMemoriesPageViewModel : HardwareListBasePageViewModel<RandomAccessMemoryViewInfo, RandomAccessMemoryInfo, GetAllRequest, GetRequest, DeleteRequest, RestoreRequest>
{
    public RandomAccessMemoriesPageViewModel(IMediator mediator,
                                             IMapper mapper,
                                             TitleBarLocator titleBarLocator,
                                             SelectedObjectLocator selectedObjectLocator,
                                             FilterOptions pagination)
        : base(mediator,
               mapper,
               titleBarLocator,
               pagination,
               selectedObjectLocator,
               Domain.Enums.HardwareType.RAM,
               TitleBarConstants.RandomAccessMemoriesPageId,
               TitleBarConstants.RandomAccessMemoryDetailsPageId) { }

    protected override void SetSelectedHardware(RandomAccessMemoryInfo hardware) =>
        SelectedObjectLocator.SelectedRandomAccessMemory = hardware;
}