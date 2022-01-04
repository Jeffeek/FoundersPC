using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class CreateRequest : PowerSupplyInfo, IRequest<PowerSupplyInfo> { }