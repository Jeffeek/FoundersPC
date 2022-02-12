using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.HardDriveDisk;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientHardDriveDiskInfo>
{
    public GetRequest() => HardwareTypeId = (int)HardwareType.HDD;
}