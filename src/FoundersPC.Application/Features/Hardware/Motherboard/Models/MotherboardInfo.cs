using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.Motherboard.Models;

public class MotherboardInfo : HardwareInfo
{
    public int? SocketId { get; set; } = default!;
    public int? MotherboardFactorId { get; set; } = default!;
    public int? RAMTypeId { get; set; } = default!;
    public int? RAMSlotsCount { get; set; } = default!;
    public int? RAMModeId { get; set; } = default!;
    public bool? CrossfireSupport { get; set; } = default!;
    public bool? SliSupport { get; set; }
    public string? AudioSupport { get; set; } = default!;
    public bool? WiFiSupport { get; set; } = default!;
    public bool? PS2Support { get; set; } = default!;
    public int? M2SlotsCount { get; set; } = default!;
    public string? PCIExpressVersion { get; set; } = default!;
}