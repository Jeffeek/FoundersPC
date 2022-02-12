using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class GetRequest : Base.GetHardwareRequest, IRequest<PowerSupplyInfo>
{
    public GetRequest() => HardwareTypeId = (int)HardwareType.PowerSupply;
}