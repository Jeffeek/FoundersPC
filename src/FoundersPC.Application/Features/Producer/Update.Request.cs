using FoundersPC.Application.Features.Producer.Models;
using MediatR;

namespace FoundersPC.Application.Features.Producer;

public class UpdateRequest : ProducerInfo, IRequest<ProducerInfo> { }