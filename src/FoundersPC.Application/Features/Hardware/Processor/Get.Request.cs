using FoundersPC.Application.Features.Hardware.Processor.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class GetRequest : IRequest<ProcessorInfo>
{
    public int Id { get; set; }
}