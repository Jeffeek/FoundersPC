using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class GetHandler : NullableGetHandler<GetRequest, ProducerInfo, Domain.Entities.Hardware.Producer, GetQuery>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }

    public override Task<ProducerInfo?> Handle(GetRequest request, CancellationToken cancellationToken) =>
        base.Handle(request, cancellationToken)
        ?? throw new NotFoundException($"Not found producer with id {request.Id}");
}