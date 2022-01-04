using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.Processor.Models;

public class ProcessorInfo : HardwareInfo
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
}