using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class MotherboardMetadata : HardwareMetadata
{
    public int? SocketId { get; set; } = default!;
    public int? MotherboardFactorId { get; set; } = default!;
    public int? RAMTypeId { get; set; } = default!;
    public int? RAMSlotsCount { get; set; } = default!;
    public int? RAMModeId { get; set; }
    public bool? CrossfireSupport { get; set; } = default!;
    public bool? SliSupport { get; set; }
    public string? AudioSupport { get; set; } = default!;
    public bool? WiFiSupport { get; set; } = default!;
    public bool? PS2Support { get; set; } = default!;
    public int? M2SlotsCount { get; set; } = default!;
    public string? PCIExpressVersion { get; set; } = default!;
    public Socket? Socket { get; set; } = default!;
    public MotherboardFactor? MotherboardFactor { get; set; } = default!;
    public RamType? RAMType { get; set; } = default!;
    public RamMode? RAMMode { get; set; } = default!;
    public Motherboard Motherboard { get; set; } = default!;
}