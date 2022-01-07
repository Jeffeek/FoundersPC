using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, MotherboardViewInfo, GetAllQuery, Domain.Entities.Hardware.Motherboard>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}