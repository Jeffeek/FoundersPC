using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.SolidStateDrive;

public class GetHandler : GetHardwareHandler<GetRequest, ClientSolidStateDriveInfo, Domain.Entities.Hardware.SolidStateDrive>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}