using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class DeleteHardwareHandler<TRequest, TQuery, THardware> : DeleteHandler<TRequest, THardware, TQuery>
    where TRequest : DeleteRequest, IRequest<Unit>
    where THardware : Domain.Entities.Hardware.Hardware
    where TQuery : GetHardwareQuery<THardware>, IQuery<THardware>
{
    protected DeleteHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper) : base(dbContextFactory, mapper) { }

    protected override string GetNotFoundString(TRequest request) =>
        $"Not found hardware {request.Id}/{request.HardwareTypeId}";
}