﻿using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public abstract class GetHardwareHandler<TRequest, TResponse, THardware> : IRequestHandler<TRequest, TResponse>
    where TRequest : GetHardwareRequest, IRequest<TResponse>
    where TResponse : ClientHardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    protected GetHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                 IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.ProjectFirstOrDefaultAsNoTrackingAsync<THardware, TResponse>(_mapper.ConfigurationProvider,
                                                                                     x => x.Id == request.Id && x.HardwareTypeId == request.HardwareTypeId,
                                                                                     cancellationToken)
               ?? throw new NotFoundException($"Not found Hardware {JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true })}");
    }
}