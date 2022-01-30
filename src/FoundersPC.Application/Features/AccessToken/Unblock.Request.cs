using MediatR;

namespace FoundersPC.Application.Features.AccessToken;

public class UnblockRequest : IRequest<Unit>
{
    public int Id { get; set; }
}