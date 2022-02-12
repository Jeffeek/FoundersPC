using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class GetRequest : Base.GetHardwareRequest, IRequest<RandomAccessMemoryInfo>
{
    public GetRequest() => HardwareTypeId = (int)HardwareType.RAM;
}