using FoundersPC.Application.Features.Client.Producer.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Producer;

public class GetRequest : IRequest<ClientProducerInfo>
{
    public int Id { get; set; }
}