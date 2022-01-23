using FoundersPC.Application.Features.Client.Hardware.Models;

namespace FoundersPC.Application.Features.Client.Hardware.HardDriveDisk.Models;

public class ClientHardDriveDiskInfo : ClientHardwareInfo
{
    public string? DiskFactor { get; set; }
    public string? DiskConnectionInterface { get; set; }
    public int? Volume { get; set; } = default!;
    public int? HeadSpeed { get; set; } = default!;
    public int? BufferSize { get; set; } = default!;
    public double? Noise { get; set; } = default!;
}