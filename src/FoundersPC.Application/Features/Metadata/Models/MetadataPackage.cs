using System.Collections.Generic;
using FoundersPC.SharedKernel.Models.Metadata;

namespace FoundersPC.Application.Features.Metadata.Models;

public class MetadataPackage
{
    public List<MetadataInfo> CaseType { get; set; }
    public List<MetadataInfo> Color { get; set; }
    public List<MetadataInfo> Country { get; set; }
    public List<MetadataInfo> DiskConnectionInterface { get; set; }
    public List<MetadataInfo> DiskFactor { get; set; }
    public List<MetadataInfo> Material { get; set; }
    public List<MetadataInfo> MotherboardFactor { get; set; }
    public List<MetadataInfo> MotherboardPowering { get; set; }
    public List<MetadataInfo> RamMode { get; set; }
    public List<MetadataInfo> RamType { get; set; }
    public List<MetadataInfo> Socket { get; set; }
    public List<MetadataInfo> TechProcess { get; set; }
    public List<MetadataInfo> VideoMemory { get; set; }
    public List<MetadataInfo> WindowMaterial { get; set; }
}