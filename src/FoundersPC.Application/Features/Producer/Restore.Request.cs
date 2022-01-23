using FoundersPC.Application.Features.Producer.Models;
using MediatR;

namespace FoundersPC.Application.Features.Producer;

public class RestoreRequest : IRequest<ProducerInfo>
{
    public int Id { get; set; }
}