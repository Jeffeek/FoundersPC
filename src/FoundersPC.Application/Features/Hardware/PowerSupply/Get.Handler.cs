using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class GetHandler : GetHardwareHandler<GetRequest, PowerSupplyInfo, GetQuery, Domain.Entities.Hardware.PowerSupply>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}