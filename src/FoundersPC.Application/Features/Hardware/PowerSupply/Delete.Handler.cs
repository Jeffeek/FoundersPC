using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class DeleteHandler : DeleteHardwareHandler<DeleteRequest, GetQuery, Domain.Entities.Hardware.PowerSupply>
{
    public DeleteHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}