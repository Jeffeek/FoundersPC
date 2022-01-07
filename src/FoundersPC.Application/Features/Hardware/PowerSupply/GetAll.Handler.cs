using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, PowerSupplyViewInfo, GetAllQuery, Domain.Entities.Hardware.PowerSupply>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}