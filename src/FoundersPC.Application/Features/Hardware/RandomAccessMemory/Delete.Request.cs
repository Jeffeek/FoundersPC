using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class DeleteRequest : Base.DeleteRequest
{
    public DeleteRequest() => HardwareTypeId = (int)HardwareType.RAM;
}