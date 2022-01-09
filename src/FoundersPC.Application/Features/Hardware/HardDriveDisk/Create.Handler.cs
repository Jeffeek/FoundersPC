using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class CreateHandler : CreateHardwareHandler<CreateRequest, HardDriveDiskInfo, Domain.Entities.Hardware.HardDriveDisk, HardDriveDiskMetadata>
{
    public CreateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}