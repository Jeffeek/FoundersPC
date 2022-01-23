using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.Metadata.Models;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Metadata;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Models.Metadata;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Metadata;

internal class GetMetadataPackageHandler : IRequestHandler<GetMetadataPackageRequest, MetadataPackage>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public GetMetadataPackageHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                     IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<MetadataPackage> Handle(GetMetadataPackageRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return new()
               {
                   CaseType = await db.Set<CaseType>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   Color = await db.Set<Color>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   Country = await db.Set<Country>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   DiskConnectionInterface = await db.Set<DiskConnectionInterface>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   DiskFactor = await db.Set<DiskFactor>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   Material = await db.Set<Material>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   MotherboardFactor = await db.Set<MotherboardFactor>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   MotherboardPowering = await db.Set<MotherboardPowering>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   RamMode = await db.Set<RamMode>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   RamType = await db.Set<RamType>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   Socket = await db.Set<Socket>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   TechProcess = await db.Set<TechProcess>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   VideoMemory = await db.Set<VideoMemory>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   WindowMaterial = await db.Set<WindowMaterial>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   Producers = await db.Set<Domain.Entities.Hardware.Producer>().ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                   IntegratedVideoCards = await db.Set<VideoCard>().Where(x => x.Metadata.IsIntegrated == true).ProjectTo<MetadataInfo>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
               };
    }
}