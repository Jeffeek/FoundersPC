using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetHardwareHandler<TRequest, TResponse, TQuery, THardware> : NullableGetHandler<TRequest, TResponse, THardware, TQuery>
    where TRequest : GetHardwareRequest, IRequest<TResponse>
    where TResponse : HardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
    where TQuery : GetHardwareQuery<THardware>, IQuery<THardware>
{
    protected GetHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                 IMapper mapper) : base(dbContextFactory, mapper) { }

    public override async Task<TResponse?> Handle(TRequest request, CancellationToken cancellationToken) =>
        await base.Handle(request, cancellationToken)
        ?? throw new NotFoundException($"Not found Hardware {JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true })}");
}