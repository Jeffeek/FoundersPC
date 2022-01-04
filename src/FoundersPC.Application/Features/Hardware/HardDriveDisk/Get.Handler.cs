using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class GetHandler : GetHardwareHandler<GetRequest, HardDriveDiskInfo, GetQuery, Domain.Entities.Hardware.HardDriveDisk>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}