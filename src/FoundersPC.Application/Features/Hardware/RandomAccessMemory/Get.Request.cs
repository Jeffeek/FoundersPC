using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class GetRequest : IRequest<RandomAccessMemoryInfo>
{
    public int Id { get; set; }
}