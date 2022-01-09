using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Case;

public class CreateHandler : CreateHardwareHandler<CreateRequest, CaseInfo, Domain.Entities.Hardware.Case, CaseMetadata>
{
    public CreateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}