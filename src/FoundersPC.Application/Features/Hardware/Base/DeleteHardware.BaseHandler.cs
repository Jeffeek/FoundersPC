using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class DeleteHardwareHandler<TRequest, THardware> : DeleteHandler<TRequest, THardware, GetHardwareQuery<THardware>>
    where TRequest : DeleteRequest, IRequest<Unit>
    where THardware : Domain.Entities.Hardware.Hardware
{
    protected DeleteHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper) : base(dbContextFactory, mapper) { }

    protected override string GetNotFoundString(TRequest request) =>
        $"Not found hardware {request.Id}/{request.HardwareTypeId}";
}