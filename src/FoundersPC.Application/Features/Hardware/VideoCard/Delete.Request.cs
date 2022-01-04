using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class DeleteRequest : Base.DeleteRequest
{
    public DeleteRequest() => HardwareTypeId = (int) HardwareType.GPU;
}