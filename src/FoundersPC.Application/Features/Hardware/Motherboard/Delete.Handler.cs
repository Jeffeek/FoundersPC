using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class DeleteHandler : DeleteHardwareHandler<DeleteRequest, GetQuery, Domain.Entities.Hardware.Motherboard>
{
    public DeleteHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}