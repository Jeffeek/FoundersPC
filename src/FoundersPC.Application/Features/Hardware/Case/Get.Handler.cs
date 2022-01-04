using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Case;

public class GetHandler : GetHardwareHandler<GetRequest, CaseInfo, GetQuery, Domain.Entities.Hardware.Case>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}