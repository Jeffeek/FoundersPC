using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class GetRequest : Base.GetHardwareRequest, IRequest<ProcessorInfo>
{
    public GetRequest() => HardwareTypeId = (int)HardwareType.CPU;
}