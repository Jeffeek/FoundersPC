using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class GetRequest : Base.GetHardwareRequest, IRequest<PowerSupplyInfo> { }