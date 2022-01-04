using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class GetRequest : IRequest<VideoCardInfo>
{
    public int Id { get; set; }
}