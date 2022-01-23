using FoundersPC.Application.Features.Client.Hardware.Models;

namespace FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory.Models;

public class ClientRandomAccessMemoryInfo : ClientHardwareInfo
{
    public string? RAMType { get; set; }
    public int? Frequency { get; set; }
    public string? Timings { get; set; }
    public double? Voltage { get; set; }
    public bool? XMP { get; set; }
    public bool? ECC { get; set; }
    public int? PCIndex { get; set; }
    public int? Volume { get; set; }
}