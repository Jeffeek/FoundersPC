using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.VideoCard.Models;

public class VideoCardInfo : HardwareInfo
{
    public int? TDP { get; set; }
    public int? AdditionalPower { get; set; }
    public int? Frequency { get; set; }
    public string? Series { get; set; }
    public int? MemoryVolume { get; set; }
    public int? VideoMemoryTypeId { get; set; }
    public int? MemoryFrequency { get; set; }
    public int? MemoryBusWidth { get; set; }
    public int? VGA { get; set; }
    public int? DVI { get; set; }
    public int? HDMI { get; set; }
    public int? DisplayPort { get; set; }
    public bool? IsIntegrated { get; set; }
}