using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.VideoCard;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.UI.Admin.Locators;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class VideoCardsPageViewModel : HardwareListBasePageViewModel<VideoCardViewInfo, VideoCardInfo, GetAllRequest, GetRequest, DeleteRequest, RestoreRequest>
{
    public VideoCardsPageViewModel(IMediator mediator,
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
               Domain.Enums.HardwareType.GPU,
               notificationHost,
               TitleBarConstants.VideoCardsPageId,
               TitleBarConstants.VideoCardDetailsPageId) { }

    protected override void SetSelectedHardware(VideoCardInfo hardware) =>
        SelectedObjectLocator.SelectedVideoCard = hardware;
}