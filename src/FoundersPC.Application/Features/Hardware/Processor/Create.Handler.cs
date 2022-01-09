using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class CreateHandler : CreateHardwareHandler<CreateRequest, ProcessorInfo, Domain.Entities.Hardware.Processor, ProcessorMetadata>
{
    public CreateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}