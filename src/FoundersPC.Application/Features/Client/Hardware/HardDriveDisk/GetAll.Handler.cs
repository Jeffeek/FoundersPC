using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.HardDriveDisk;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, ClientHardDriveDiskInfo, GetAllQuery, Domain.Entities.Hardware.HardDriveDisk>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}