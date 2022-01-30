using MediatR;

namespace FoundersPC.Application.Features.AccessToken;

public class BlockRequest : IRequest<Unit>
{
    public int Id { get; set; }
}