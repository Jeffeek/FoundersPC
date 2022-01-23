using FoundersPC.Application.Features.Client.Hardware.Processor.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.Processor;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientProcessorInfo> { }