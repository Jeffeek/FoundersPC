using FoundersPC.Application.Features.Producer.Models;
using MediatR;

namespace FoundersPC.Application.Features.Producer;

public class CreateRequest : ProducerInfo, IRequest<ProducerInfo> { }