using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Client.Producer.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Producer;

public class GetHandler : IRequestHandler<GetRequest, ClientProducerInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<ClientProducerInfo> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.ProjectFirstOrDefaultAsNoTrackingAsync<Domain.Entities.Hardware.Producer, ClientProducerInfo>(_mapper.ConfigurationProvider, x => x.Id == request.Id, cancellationToken)
               ?? throw new NotFoundException($"Not found Producer with Id {request.Id}");
    }
}