using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class RandomAccessMemoryMetadata : HardwareMetadata
{
    public int? RAMTypeId { get; set; } = default!;
    public int? Frequency { get; set; } = default!;
    public string? Timings { get; set; } = default!;
    public double? Voltage { get; set; } = default!;
    public bool? XMP { get; set; } = default!;
    public bool? ECC { get; set; } = default!;
    public int? PCIndex { get; set; } = default!;
    public int? Volume { get; set; } = default!;
    public RAMType? RAMType { get; set; }
    public RandomAccessMemory RandomAccessMemory { get; set; } = default!;
}