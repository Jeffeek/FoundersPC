using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.VideoCard.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.VideoCard;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ClientVideoCardInfo>>
{
    public bool? IsIntegrated { get; set; }
}