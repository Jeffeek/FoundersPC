using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.SolidStateDrive;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, ClientSolidStateDriveInfo, GetAllQuery, Domain.Entities.Hardware.SolidStateDrive>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}