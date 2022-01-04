using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class GetRequest : IRequest<MotherboardInfo>
{
    public int Id { get; set; }
}