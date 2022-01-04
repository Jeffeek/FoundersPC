using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.VideoCard.Models;

public class VideoCardInfo : HardwareInfo
{
    public int? TDP { get; set; }
    public int? AdditionalPower { get; set; } = default!;
    public int? Frequency { get; set; } = default!;
    public string? Series { get; set; } = default!;
    public int? MemoryVolume { get; set; } = default!;
    public int? VideoMemoryTypeId { get; set; } = default!;
    public int? MemoryFrequency { get; set; } = default!;
    public int? MemoryBusWidth { get; set; } = default!;
    public int? VGA { get; set; } = default!;
    public int? DVI { get; set; } = default!;
    public int? HDMI { get; set; } = default!;
    public int? DisplayPort { get; set; } = default!;
}