using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.Case;

public class DeleteRequest : Base.DeleteRequest
{
    public DeleteRequest() => HardwareTypeId = (int)HardwareType.Case;
}