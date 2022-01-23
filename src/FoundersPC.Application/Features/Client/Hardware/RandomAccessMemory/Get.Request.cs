using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientRandomAccessMemoryInfo> { }