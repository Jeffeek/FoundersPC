using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class VideoCardMetadata : HardwareMetadata
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
    public VideoMemory? VideoMemoryType { get; set; }
    public VideoCard VideoCard { get; set; } = default!;
}