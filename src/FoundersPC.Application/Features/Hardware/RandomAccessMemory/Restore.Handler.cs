using AutoMapper;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class RestoreHandler : Base.RestoreHardwareHandler<RestoreRequest, Domain.Entities.Hardware.RandomAccessMemory, RandomAccessMemoryInfo, GetQuery>
{
    public RestoreHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                          IMapper mapper)
        : base(dbContextFactory,
               mapper) { }
}