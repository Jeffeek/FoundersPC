using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public class RestoreHardwareHandler<TRestoreRequest, THardware, THardwareInfo> : IRequestHandler<TRestoreRequest, THardwareInfo>
    where TRestoreRequest : RestoreRequest, IRequest<THardwareInfo>
    where THardwareInfo : HardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    protected RestoreHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                     IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<THardwareInfo> Handle(TRestoreRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var entity = await db.Set<THardware>()
                             .FirstOrDefaultAsync(x => x.Id == request.Id && x.HardwareTypeId == request.HardwareTypeId,
                                                  cancellationToken)
                     ?? throw new NotFoundException($"Not found Hardware with Type {request.HardwareTypeId} and Id {request.Id}");

        entity.IsDeleted = false;
        entity.DeletedById = null;
        entity.Deleted = null;

        await db.CommitTransactionAsync(cancellationToken);

        return await db.ProjectFirstAsNoTrackingAsync<THardware, THardwareInfo>(_mapper.ConfigurationProvider,
                                                                                x => x.Id == request.Id && x.HardwareTypeId == request.HardwareTypeId,
                                                                                cancellationToken);
    }
}