using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class UpdateHardwareHandler<TRequest, TResponse, TQuery, THardware> : UpdateHandler<TRequest, TResponse, THardware, TQuery>
    where TRequest : HardwareInfo, IRequest<TResponse>
    where TResponse : HardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
    where TQuery : GetHardwareQuery<THardware>, IQuery<THardware>
{
    protected UpdateHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper) : base(dbContextFactory, mapper) { }

    protected override string GetNotFoundString(TRequest request) =>
        $"Not found Hardware with [Type Id]: {request.HardwareTypeId} and [Id]: {request.Id}";
}