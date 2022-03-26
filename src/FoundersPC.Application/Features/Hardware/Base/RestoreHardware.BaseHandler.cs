using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public class RestoreHardwareHandler<TRestoreRequest, THardware, THardwareInfo> : RestoreHandler<TRestoreRequest, THardwareInfo, THardware, GetHardwareQuery<THardware>>
    where TRestoreRequest : RestoreRequest, IRequest<THardwareInfo>
    where THardwareInfo : HardwareInfo
    where THardware : Domain.Entities.Hardware.Hardware
{
    protected RestoreHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                     IMapper mapper) : base(dbContextFactory, mapper) { }

    protected override string GetNotFoundString(TRestoreRequest request) =>
        $"Not found hardware with id {request.Id} and type {request.HardwareTypeId}";
}