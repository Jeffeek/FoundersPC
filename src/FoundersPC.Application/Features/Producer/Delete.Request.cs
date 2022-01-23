using MediatR;

namespace FoundersPC.Application.Features.Producer;

public class DeleteRequest : IRequest<Unit>
{
    public int Id { get; set; }
}