using FoundersPC.Application.Features.Client.Hardware.Motherboard.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.Motherboard;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientMotherboardInfo>
{
    public GetRequest() => HardwareTypeId = (int)HardwareType.MB;
}