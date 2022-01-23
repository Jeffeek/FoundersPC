using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.Processor.Models;

public class ProcessorInfo : HardwareInfo
{
    public int? TDP { get; set; }
    public string? Series { get; set; }
    public int? MaxRamSpeed { get; set; }
    public int? CoresCount { get; set; }
    public int? ThreadsCount { get; set; }
    public int? Frequency { get; set; }
    public int? TurboBoostFrequency { get; set; }
    public int? TechProcessId { get; set; }
    public int? L1Cache { get; set; }
    public int? L2Cache { get; set; }
    public int? L3Cache { get; set; }
    public int? IntegratedGraphicsId { get; set; }
    public int? SocketId { get; set; }
}