using Amusoft.UI.WPF.Notifications;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.Case;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class CaseDetailsPageViewModel : HardwareDetailsPageViewModel<CaseInfo, CaseInfoViewModel, CreateRequest, UpdateRequest>
{
    public CaseDetailsPageViewModel(IMediator mediator,
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
               TitleBarConstants.CaseDetailsPageId,
               TitleBarConstants.CasesPageId) { }

    protected override void SubscribeToHardwareLocator() =>
        SelectedObjectLocator.SelectedCaseChanged += OnSelectedHardwareChanged;
}