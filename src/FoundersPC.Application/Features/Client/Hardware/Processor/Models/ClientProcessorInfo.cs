using FoundersPC.Application.Features.Client.Hardware.Models;
using FoundersPC.Application.Features.Client.Hardware.VideoCard.Models;

namespace FoundersPC.Application.Features.Client.Hardware.Processor.Models;

public class ClientProcessorInfo : ClientHardwareInfo
{
    public int? TDP { get; set; }
    public string? Series { get; set; }
    public int? MaxRamSpeed { get; set; }
    public int? CoresCount { get; set; }
    public int? ThreadsCount { get; set; }
    public int? Frequency { get; set; }
    public int? TurboBoostFrequency { get; set; }
    public string? TechProcess { get; set; }
    public int? L1Cache { get; set; }
    public int? L2Cache { get; set; }
    public int? L3Cache { get; set; }
    public ClientVideoCardInfo? IntegratedGraphics { get; set; }
    public string? Socket { get; set; }
}