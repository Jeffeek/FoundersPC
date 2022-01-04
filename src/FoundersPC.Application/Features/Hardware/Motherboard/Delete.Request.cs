using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class DeleteRequest : Base.DeleteRequest
{
    public DeleteRequest() => HardwareTypeId = (int)HardwareType.MB;
}