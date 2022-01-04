using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class UpdateRequest : VideoCardInfo, IRequest<VideoCardInfo> { }