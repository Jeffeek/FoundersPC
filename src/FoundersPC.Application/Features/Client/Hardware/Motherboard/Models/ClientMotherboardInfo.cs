using FoundersPC.Application.Features.Client.Hardware.Models;

namespace FoundersPC.Application.Features.Client.Hardware.Motherboard.Models;

public class ClientMotherboardInfo : ClientHardwareInfo
{
    public string? Socket { get; set; } = default!;
    public string? MotherboardFactor { get; set; } = default!;
    public string? RAMType { get; set; } = default!;
    public int? RAMSlotsCount { get; set; } = default!;
    public string? RAMMode { get; set; } = default!;
    public bool? CrossfireSupport { get; set; } = default!;
    public bool? SliSupport { get; set; }
    public string? AudioSupport { get; set; } = default!;
    public bool? WiFiSupport { get; set; } = default!;
    public bool? PS2Support { get; set; } = default!;
    public int? M2SlotsCount { get; set; } = default!;
    public string? PCIExpressVersion { get; set; } = default!;
}