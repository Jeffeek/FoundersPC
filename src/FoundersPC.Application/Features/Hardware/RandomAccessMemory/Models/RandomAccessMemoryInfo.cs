using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;

public class RandomAccessMemoryInfo : HardwareInfo
{
    public int? RAMTypeId { get; set; }
    public int? Frequency { get; set; }
    public string? Timings { get; set; }
    public double? Voltage { get; set; }
    public bool? XMP { get; set; }
    public bool? ECC { get; set; }
    public int? PCIndex { get; set; }
    public int? Volume { get; set; }
}