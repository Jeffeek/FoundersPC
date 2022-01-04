using FoundersPC.Application.Features.Hardware.Processor.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class CreateRequest : ProcessorInfo, IRequest<ProcessorInfo> { }