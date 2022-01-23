using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<VideoCardViewInfo>>
{
    public bool? IsIntegrated { get; set; }
}