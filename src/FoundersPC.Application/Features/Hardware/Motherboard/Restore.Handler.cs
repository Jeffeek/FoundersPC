using AutoMapper;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class RestoreHandler : Base.RestoreHardwareHandler<RestoreRequest, Domain.Entities.Hardware.Motherboard, MotherboardInfo, GetQuery>
{
    public RestoreHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                          IMapper mapper)
        : base(dbContextFactory,
               mapper) { }
}