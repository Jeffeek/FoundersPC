using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class CreateRequest : RandomAccessMemoryInfo, IRequest<RandomAccessMemoryInfo> { }