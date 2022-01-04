using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.Case.Models;

public class CaseInfo : HardwareInfo
{
    public int? WindowMaterialId { get; set; } = default!;
    public int? CaseTypeId { get; set; }
    public double? MaxMotherboardSize { get; set; } = default!;
    public int? MaterialId { get; set; } = default!;
    public bool? TransparentWindow { get; set; } = default!;
    public int? ColorId { get; set; } = default!;
    public double? Weight { get; set; } = default!;
    public double? Height { get; set; } = default!;
    public double? Width { get; set; } = default!;
    public double? Depth { get; set; } = default!;
}