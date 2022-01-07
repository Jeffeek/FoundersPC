using MediatR;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class DeleteRequest : IRequest<Unit>
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }
}