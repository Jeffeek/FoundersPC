using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, ProcessorViewInfo, GetAllQuery, Domain.Entities.Hardware.Processor>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}