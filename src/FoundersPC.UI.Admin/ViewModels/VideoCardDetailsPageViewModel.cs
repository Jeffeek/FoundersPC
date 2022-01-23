using AutoMapper;
using FoundersPC.Application.Features.Hardware.VideoCard;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class VideoCardDetailsPageViewModel : HardwareDetailsPageViewModel<VideoCardInfo, VideoCardInfoViewModel, CreateRequest, UpdateRequest>
{
    public VideoCardDetailsPageViewModel(IMediator mediator,
                                         IMapper mapper,
                                         SelectedObjectLocator selectedObjectLocator,
                                         MetadataPackageLocator metadataPackageLocator,
                                         TitleBarLocator titleBarLocator)
        : base(mediator,
               mapper,
               selectedObjectLocator,
               metadataPackageLocator,
               titleBarLocator,
               TitleBarConstants.VideoCardDetailsPageId,
               TitleBarConstants.VideoCardsPageId) { }

    protected override void SubscribeToHardwareLocator() =>
        SelectedObjectLocator.SelectedVideoCardChanged += OnSelectedHardwareChanged;
}