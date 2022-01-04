using FoundersPC.Application.Features.Hardware.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware;

public class GetHardwareRequest : IRequest<HardwareInfo>
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }
}