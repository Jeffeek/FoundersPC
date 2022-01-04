using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class CreateHardwareHandler<TRequest, TResponse, THardware> : IRequestHandler<TRequest, TResponse>
    where TRequest : HardwareInfo, IRequest<TResponse>
    where TResponse : HardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    protected CreateHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var entity = await db.Set<THardware>()
                             .Persist(_mapper)
                             .InsertOrUpdateAsync(request, cancellationToken);

        await db.CommitTransactionAsync(cancellationToken);

        return await db.Set<THardware>()
                       .AsNoTracking()
                       .Where(x => x.Id == entity.Id && x.HardwareTypeId == entity.HardwareTypeId)
                       .ProjectTo<TResponse>(_mapper.ConfigurationProvider)
                       .FirstAsync(cancellationToken);
    }
}