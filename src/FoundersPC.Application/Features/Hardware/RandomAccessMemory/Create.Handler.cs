using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class CreateHandler : CreateHardwareHandler<CreateRequest, RandomAccessMemoryInfo, Domain.Entities.Hardware.RandomAccessMemory, RandomAccessMemoryMetadata>
{
    public CreateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}