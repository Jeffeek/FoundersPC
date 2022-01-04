using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class GetHandler : GetHardwareHandler<GetRequest, MotherboardInfo, GetQuery, Domain.Entities.Hardware.Motherboard>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}