using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Client.Producer.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Producer;

public class GetHandler : NullableGetHandler<GetRequest, ClientProducerInfo, Domain.Entities.Hardware.Producer, GetQuery>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }

    public override async Task<ClientProducerInfo?> Handle(GetRequest request, CancellationToken cancellationToken) =>
        await base.Handle(request, cancellationToken)
        ?? throw new NotFoundException($"Not found Producer with Id {request.Id}");
}