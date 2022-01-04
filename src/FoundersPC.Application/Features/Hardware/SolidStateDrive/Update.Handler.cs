using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class UpdateHandler : UpdateHardwareHandler<UpdateRequest, SolidStateDriveInfo, GetQuery, Domain.Entities.Hardware.SolidStateDrive>
{
    public UpdateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}