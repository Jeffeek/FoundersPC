using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class ProcessorMetadata : HardwareMetadata
{
    public int? TDP { get; set; } = default!;
    public string? Series { get; set; } = default!;
    public int? MaxRamSpeed { get; set; } = default!;
    public int? CoresCount { get; set; } = default!;
    public int? ThreadsCount { get; set; } = default!;
    public int? Frequency { get; set; } = default!;
    public int? TurboBoostFrequency { get; set; } = default!;
    public int? TechProcessId { get; set; } = default!;
    public int? L1Cache { get; set; } = default!;
    public int? L2Cache { get; set; } = default!;
    public int? L3Cache { get; set; } = default!;
    public int? IntegratedGraphicsId { get; set; } = default!;
    public TechProcess? TechProcess { get; set; }
    public VideoCard? IntegratedGraphics { get; set; }
    public Processor Processor { get; set; } = default!;
}