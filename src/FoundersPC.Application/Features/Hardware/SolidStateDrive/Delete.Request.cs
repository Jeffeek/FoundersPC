using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class DeleteRequest : Base.DeleteRequest
{
    public DeleteRequest() => HardwareTypeId = (int) HardwareType.SSD;
}