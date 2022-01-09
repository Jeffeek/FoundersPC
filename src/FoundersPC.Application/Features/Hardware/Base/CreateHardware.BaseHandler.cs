using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class CreateHardwareHandler<TRequest, TResponse, THardware, THardwareMetadata> : IRequestHandler<TRequest, TResponse>
    where TRequest : HardwareInfo, IRequest<TResponse>
    where TResponse : HardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
    where THardwareMetadata : HardwareMetadata
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

        var hardware = _mapper.Map<THardware>(request);

        await db.Set<THardware>()
                .AddAsync(hardware, cancellationToken);

        await db.SaveChangesAsync(cancellationToken);

        var metadata = _mapper.Map<THardwareMetadata>(request);

        hardware.BaseMetadata = metadata;
        metadata.Id = hardware.Id;
        metadata.Hardware = hardware;

        await db.Set<THardwareMetadata>()
                .AddAsync(metadata, cancellationToken);

        await db.CommitTransactionAsync(cancellationToken);

        return await db.ProjectFirstAsNoTrackingAsync<THardware, TResponse>(_mapper.ConfigurationProvider,
                                                                            x => x.Id == hardware.Id && x.HardwareTypeId == request.HardwareTypeId,
                                                                            cancellationToken);
    }
}