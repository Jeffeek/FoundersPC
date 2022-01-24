using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class DeleteRequest : Base.DeleteRequest
{
    public DeleteRequest() => HardwareTypeId = (int)HardwareType.PowerSupply;
}