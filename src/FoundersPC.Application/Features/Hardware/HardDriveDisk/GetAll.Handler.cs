using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, HardDriveDiskViewInfo, GetAllQuery, Domain.Entities.Hardware.HardDriveDisk>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}