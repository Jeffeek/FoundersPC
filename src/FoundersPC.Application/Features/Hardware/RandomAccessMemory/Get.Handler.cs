using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class GetHandler : GetHardwareHandler<GetRequest, RandomAccessMemoryInfo, GetQuery, Domain.Entities.Hardware.RandomAccessMemory>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}