using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class GetRequest : Base.GetHardwareRequest, IRequest<VideoCardInfo>
{
    public GetRequest() => HardwareTypeId = (int)HardwareType.GPU;
}