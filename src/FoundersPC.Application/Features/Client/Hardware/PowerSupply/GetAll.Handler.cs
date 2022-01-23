using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.PowerSupply.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.PowerSupply;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, ClientPowerSupplyInfo, GetAllQuery, Domain.Entities.Hardware.PowerSupply>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}