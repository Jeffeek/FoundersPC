using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class UpdateHandler : IRequestHandler<UpdateRequest, ProducerInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public UpdateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<ProducerInfo> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        _ = await db.Set<Domain.Entities.Hardware.Producer>()
                    .ApplyQuery(_mapper.Map<GetQuery>(request))
                    .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException($"Not found Producer with [Id]: {request.Id}");

        await db.Set<Domain.Entities.Hardware.Producer>()
                .Persist(_mapper)
                .InsertOrUpdateAsync(request, cancellationToken);

        await db.CommitTransactionAsync(cancellationToken);

        return await db.ProjectFirstAsNoTrackingAsync<Domain.Entities.Hardware.Producer, ProducerInfo>(_mapper.ConfigurationProvider,
                                                                                                       x => x.Id == request.Id,
                                                                                                       cancellationToken);
    }
}