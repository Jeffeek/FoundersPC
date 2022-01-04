using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class GetHandler : GetHardwareHandler<GetRequest, SolidStateDriveInfo, GetQuery, Domain.Entities.Hardware.SolidStateDrive>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}