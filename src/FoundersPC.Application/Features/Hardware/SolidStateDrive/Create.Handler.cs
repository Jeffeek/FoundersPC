using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class CreateHandler : CreateHardwareHandler<CreateRequest, SolidStateDriveInfo, Domain.Entities.Hardware.SolidStateDrive, SolidStateDriveMetadata>
{
    public CreateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}