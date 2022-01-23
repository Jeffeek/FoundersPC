using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory;

public class GetHandler : GetHardwareHandler<GetRequest, ClientRandomAccessMemoryInfo, Domain.Entities.Hardware.RandomAccessMemory>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}