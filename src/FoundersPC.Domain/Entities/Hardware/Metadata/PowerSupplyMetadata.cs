using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class PowerSupplyMetadata : HardwareMetadata
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
    public MotherboardPowering? MotherboardPowering { get; set; } = default!;
    public PowerSupply PowerSupply { get; set; } = default!;
}