using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, SolidStateDriveViewInfo, GetAllQuery, Domain.Entities.Hardware.SolidStateDrive>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}