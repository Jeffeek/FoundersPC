using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;

public class RandomAccessMemoryInfo : HardwareInfo
{
    public int? RAMTypeId { get; set; } = default!;
    public int? Frequency { get; set; } = default!;
    public string? Timings { get; set; } = default!;
    public double? Voltage { get; set; } = default!;
    public bool? XMP { get; set; } = default!;
    public bool? ECC { get; set; } = default!;
    public int? PCIndex { get; set; } = default!;
    public int? Volume { get; set; } = default!;
}