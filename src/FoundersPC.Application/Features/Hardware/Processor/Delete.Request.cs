using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class DeleteRequest : Base.DeleteRequest
{
    public DeleteRequest() => HardwareTypeId = (int)HardwareType.CPU;
}