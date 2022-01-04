using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class UpdateHandler : UpdateHardwareHandler<UpdateRequest, HardDriveDiskInfo, GetQuery, Domain.Entities.Hardware.HardDriveDisk>
{
    public UpdateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}