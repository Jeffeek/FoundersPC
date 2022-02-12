using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class GetRequest : Base.GetHardwareRequest, IRequest<SolidStateDriveInfo>
{
    public GetRequest() => HardwareTypeId = (int)HardwareType.SSD;
}