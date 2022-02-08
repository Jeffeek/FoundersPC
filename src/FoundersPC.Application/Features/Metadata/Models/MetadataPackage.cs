using System.Collections.Generic;
using FoundersPC.SharedKernel.Models.Metadata;

namespace FoundersPC.Application.Features.Metadata.Models;

public class MetadataPackage
{
    public List<MetadataInfo> CaseType { get; set; } = default!;
    public List<MetadataInfo> Color { get; set; } = default!;
    public List<MetadataInfo> Country { get; set; } = default!;
    public List<MetadataInfo> DiskConnectionInterface { get; set; } = default!;
    public List<MetadataInfo> DiskFactor { get; set; } = default!;
    public List<MetadataInfo> Material { get; set; } = default!;
    public List<MetadataInfo> MotherboardFactor { get; set; } = default!;
    public List<MetadataInfo> MotherboardPowering { get; set; } = default!;
    public List<MetadataInfo> RamMode { get; set; } = default!;
    public List<MetadataInfo> RamType { get; set; } = default!;
    public List<MetadataInfo> Socket { get; set; } = default!;
    public List<MetadataInfo> TechProcess { get; set; } = default!;
    public List<MetadataInfo> VideoMemory { get; set; } = default!;
    public List<MetadataInfo> WindowMaterial { get; set; } = default!;
    public List<MetadataInfo> Producers { get; set; } = default!;
    public List<MetadataInfo> IntegratedVideoCards { get; set; } = default!;
}