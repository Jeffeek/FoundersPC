using AutoMapper;
using FoundersPC.Application.Features.Hardware.Case;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.UI.Admin.Locators;
using MediatR;

namespace FoundersPC.UI.Admin.ViewModels;

public class CasesPageViewModel : HardwareListBasePageViewModel<CaseViewInfo, CaseInfo, GetAllRequest, GetRequest, DeleteRequest, RestoreRequest>
{
    public CasesPageViewModel(IMediator mediator,
                              IMapper mapper,
                              TitleBarLocator titleBarLocator,
                              SelectedObjectLocator selectedObjectLocator,
                              FilterOptions pagination)
        : base(mediator,
               mapper,
               titleBarLocator,
               pagination,
               selectedObjectLocator,
               Domain.Enums.HardwareType.Case,
               TitleBarConstants.CasesPageId,
               TitleBarConstants.CaseDetailsPageId) { }

    protected override void SetSelectedHardware(CaseInfo hardware) =>
        SelectedObjectLocator.SelectedCase = hardware;
}