using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class DeleteHandler : DeleteHardwareHandler<DeleteRequest, GetQuery, Domain.Entities.Hardware.Processor>
{
    public DeleteHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}