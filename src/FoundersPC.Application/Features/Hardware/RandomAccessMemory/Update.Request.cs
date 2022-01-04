using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class UpdateRequest : RandomAccessMemoryInfo, IRequest<RandomAccessMemoryInfo> { }