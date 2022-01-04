using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class DeleteRequest : Base.DeleteRequest
{
    public DeleteRequest() => HardwareTypeId = (int)HardwareType.HDD;
}