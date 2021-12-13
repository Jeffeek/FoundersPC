using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class VideoCardMetadata : HardwareMetadata
{
    public int? TDP { get; set; }
    public int? AdditionalPower { get; set; } = default!;
    public int? Frequency { get; set; } = default!;
    public string? Series { get; set; } = default!;
    public int? MemoryVolume { get; set; } = default!;
    public int? VideoMemoryId { get; set; } = default!;
    public int? MemoryFrequency { get; set; } = default!;
    public int? MemoryBusWidth { get; set; } = default!;
    public VideoMemory? VideoMemory { get; set; } = default!;
    public int? VGA { get; set; } = default!;
    public int? DVI { get; set; } = default!;
    public int? HDMI { get; set; } = default!;
    public int? DisplayPort { get; set; } = default!;
    public VideoCard VideoCard { get; set; } = default!;
}