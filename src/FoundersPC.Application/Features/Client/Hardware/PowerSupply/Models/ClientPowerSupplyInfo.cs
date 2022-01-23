using FoundersPC.Application.Features.Client.Hardware.Models;

namespace FoundersPC.Application.Features.Client.Hardware.PowerSupply.Models;

public class ClientPowerSupplyInfo : ClientHardwareInfo
{
    public int? Power { get; set; } = default!;
    public int? Efficiency { get; set; } = default!;
    public string? MotherboardPowering { get; set; } = default!;
    public bool? IsModular { get; set; } = default!;
    public bool? CPU4PIN { get; set; } = default!;
    public bool? CPU8PIN { get; set; } = default!;
    public int? FanDiameter { get; set; } = default!;
    public bool? Certificate80PLUS { get; set; } = default!;
    public bool? PFC { get; set; } = default!;
}