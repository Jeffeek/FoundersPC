using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, ClientRandomAccessMemoryInfo, GetAllQuery, Domain.Entities.Hardware.RandomAccessMemory>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}