using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Client.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public abstract class GetHardwareHandler<TRequest, TResponse, THardware, TQuery> : NullableGetHandler<TRequest, TResponse, THardware, TQuery>
    where TRequest : GetHardwareRequest, IRequest<TResponse>
    where TResponse : ClientHardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
    where TQuery : class, IQuery<THardware>
{
    protected GetHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                 IMapper mapper) : base(dbContextFactory, mapper) { }

    public override async Task<TResponse?> Handle(TRequest request, CancellationToken cancellationToken) =>
        await base.Handle(request, cancellationToken)
        ?? throw new NotFoundException($"Not found Hardware: {request.Id}/{request.HardwareTypeId}");
}