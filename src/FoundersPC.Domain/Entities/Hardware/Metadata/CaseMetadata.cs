using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class CaseMetadata : HardwareMetadata
{
    public string? WindowMaterial { get; set; } = default!;
    public int? CaseTypeId { get; set; }
    public double? MaxMotherboardSize { get; set; } = default!;
    public string? Material { get; set; } = default!;
    public bool? TransparentWindow { get; set; } = default!;
    public string? Color { get; set; } = default!;
    public double? Weight { get; set; } = default!;
    public double? Height { get; set; } = default!;
    public double? Width { get; set; } = default!;
    public double? Depth { get; set; } = default!;
    public CaseType? CaseType { get; set; } = default!;
    public Case Case { get; set; } = default!;
}