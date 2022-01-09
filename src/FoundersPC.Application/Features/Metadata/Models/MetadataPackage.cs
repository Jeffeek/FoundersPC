using System.Collections;
using System.Collections.Generic;
using FoundersPC.SharedKernel.Models.Metadata;

namespace FoundersPC.Application.Features.Metadata.Models;

public class MetadataPackage : IEnumerable<List<MetadataInfo>>
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
    public List<MetadataInfo> Producer { get; set; } = default!;

    public IEnumerator<List<MetadataInfo>> GetEnumerator()
    {
        yield return CaseType;
        yield return Color;
        yield return Country;
        yield return DiskConnectionInterface;
        yield return DiskFactor;
        yield return Material;
        yield return MotherboardFactor;
        yield return MotherboardPowering;
        yield return RamMode;
        yield return RamType;
        yield return Socket;
        yield return TechProcess;
        yield return VideoMemory;
        yield return WindowMaterial;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}