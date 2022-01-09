using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class GetRequest : Base.GetHardwareRequest, IRequest<VideoCardInfo> { }