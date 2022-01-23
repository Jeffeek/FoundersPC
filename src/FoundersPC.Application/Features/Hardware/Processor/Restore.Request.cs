using FoundersPC.Application.Features.Hardware.Processor.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class RestoreRequest : Base.RestoreRequest, IRequest<ProcessorInfo> { }