using MediatR;

namespace FoundersPC.Application.Features.UserInformation;

public class BlockRequest : IRequest<Unit>
{
    public int Id { get; set; }
    public string? Reason { get; set; }
}