using FoundersPC.Application.Features.Client.Hardware.VideoCard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.VideoCard;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientVideoCardInfo> { }