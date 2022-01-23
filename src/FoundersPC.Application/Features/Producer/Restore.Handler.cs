using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class RestoreHandler : IRequestHandler<RestoreRequest, ProducerInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public RestoreHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                          IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<ProducerInfo> Handle(RestoreRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var producer = await db.Set<Domain.Entities.Hardware.Producer>()
                               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                       ?? throw new NotFoundException($"Not found Producer with Id {request.Id}");

        producer.DeletedBy = null;
        producer.Deleted = null;
        producer.IsDeleted = false;
        producer.DeletedById = null;

        await db.CommitTransactionAsync(cancellationToken);

        return await db.ProjectFirstAsNoTrackingAsync<Domain.Entities.Hardware.Producer, ProducerInfo>(_mapper.ConfigurationProvider, x => x.Id == request.Id, cancellationToken);
    }
}