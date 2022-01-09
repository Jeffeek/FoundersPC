using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class CreateHandler : CreateHardwareHandler<CreateRequest, PowerSupplyInfo, Domain.Entities.Hardware.PowerSupply, PowerSupplyMetadata>
{
    public CreateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}