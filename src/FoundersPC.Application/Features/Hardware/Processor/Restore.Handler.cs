using AutoMapper;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class RestoreHandler : Base.RestoreHardwareHandler<RestoreRequest, Domain.Entities.Hardware.Processor, ProcessorInfo>
{
    public RestoreHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                          IMapper mapper)
        : base(dbContextFactory,
               mapper) { }
}