using FoundersPC.Application.Features.Client.Hardware.PowerSupply.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.PowerSupply;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientPowerSupplyInfo> { }