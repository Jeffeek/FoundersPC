using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.PowerSupply.Models;

public class PowerSupplyInfo : HardwareInfo
{
    public int? Power { get; set; } = default!;
    public int? Efficiency { get; set; } = default!;
    public int? MotherboardPoweringId { get; set; } = default!;
    public bool? IsModular { get; set; } = default!;
    public bool? CPU4PIN { get; set; } = default!;
    public bool? CPU8PIN { get; set; } = default!;
    public int? FanDiameter { get; set; } = default!;
    public bool? Certificate80PLUS { get; set; } = default!;
    public bool? PFC { get; set; } = default!;
}