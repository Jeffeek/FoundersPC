using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class UpdateHardwareHandler<TRequest, TResponse, TQuery, THardware> : IRequestHandler<TRequest, TResponse>
    where TRequest : TResponse, IRequest<TResponse>
    where TResponse : HardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
    where TQuery : GetHardwareQuery<THardware>, IQuery<THardware>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    protected UpdateHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        _ = await db.Set<THardware>()
                    .ApplyQuery(_mapper.Map<TQuery>(request))
                    .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException($"Not found Hardware with [Type Id]: {request.HardwareTypeId} and [Id]: {request.Id}");

        await db.Set<THardware>()
                .Persist(_mapper)
                .InsertOrUpdateAsync(request, cancellationToken);

        await db.CommitTransactionAsync(cancellationToken);

        return await db.Set<THardware>()
                       .AsNoTracking()
                       .ApplyQuery(_mapper.Map<TQuery>(request))
                       .ProjectTo<TResponse>(_mapper.ConfigurationProvider)
                       .FirstAsync(cancellationToken);
    }
}