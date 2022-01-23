using FoundersPC.Application.Features.Client.Hardware.Models;

namespace FoundersPC.Application.Features.Client.Hardware.Case.Models;

public class ClientCaseInfo : ClientHardwareInfo
{
    public string? WindowMaterial { get; set; } = default!;
    public string? CaseType { get; set; }
    public double? MaxMotherboardSize { get; set; } = default!;
    public string? Material { get; set; } = default!;
    public bool? TransparentWindow { get; set; } = default!;
    public string? Color { get; set; } = default!;
    public double? Weight { get; set; } = default!;
    public double? Height { get; set; } = default!;
    public double? Width { get; set; } = default!;
    public double? Depth { get; set; } = default!;
}