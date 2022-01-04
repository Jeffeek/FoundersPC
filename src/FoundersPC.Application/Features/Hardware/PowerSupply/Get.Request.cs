using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class GetRequest : IRequest<PowerSupplyInfo>
{
    public int Id { get; set; }
}