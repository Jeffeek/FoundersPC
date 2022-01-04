using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class DeleteHardwareHandler<TRequest, TQuery, THardware> : IRequestHandler<TRequest, Unit>
    where TRequest : DeleteRequest, IRequest<Unit>
    where THardware : Domain.Entities.Hardware.Hardware
    where TQuery : GetHardwareQuery<THardware>, IQuery<THardware>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    protected DeleteHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var entity = await db.Set<THardware>()
                             .ApplyQuery(_mapper.Map<TQuery>(request))
                             .FirstOrDefaultAsync(cancellationToken)
                     ?? throw new NotFoundException($"Not found Hardware with [Type Id]: {request.HardwareTypeId} and [Id]: {request.Id}");

        db.Set<THardware>()
          .Remove(entity);

        await db.CommitTransactionAsync(cancellationToken);

        return Unit.Value;
    }
}