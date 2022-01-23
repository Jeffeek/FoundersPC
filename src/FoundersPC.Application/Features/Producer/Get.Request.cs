using FoundersPC.Application.Features.Producer.Models;
using MediatR;

namespace FoundersPC.Application.Features.Producer;

public class GetRequest : IRequest<ProducerInfo>
{
    public int Id { get; set; }
}