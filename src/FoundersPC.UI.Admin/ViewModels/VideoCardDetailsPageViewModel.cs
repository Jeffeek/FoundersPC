using System.Linq;
using Amusoft.UI.WPF.Notifications;
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
                                         TitleBarLocator titleBarLocator,
                                         NotificationHost notificationHost)
        : base(mediator,
               mapper,
               selectedObjectLocator,
               metadataPackageLocator,
               titleBarLocator,
               notificationHost,
               TitleBarConstants.VideoCardDetailsPageId,
               TitleBarConstants.VideoCardsPageId) { }

    protected override void SubscribeToHardwareLocator() =>
        SelectedObjectLocator.SelectedVideoCardChanged += OnSelectedHardwareChanged;

    protected override void OnInsertOrUpdate(int id)
    {
        if (EditableHardware == null)
            return;

        var existVideoCard = MetadataPackageLocator.MetadataPackage.IntegratedVideoCards.FirstOrDefault(x => x.Id == id);

        if (EditableHardware.IsIntegrated == true && existVideoCard == null)
            MetadataPackageLocator.MetadataPackage.IntegratedVideoCards.Add(new()
                                                                            {
                                                                                Id = id,
                                                                                Value = EditableHardware.Title
                                                                            });

        if (EditableHardware.Id != 0
            && EditableHardware.IsIntegrated is null or false
            && existVideoCard != null)
            MetadataPackageLocator.MetadataPackage.IntegratedVideoCards.Remove(existVideoCard);
    }
}