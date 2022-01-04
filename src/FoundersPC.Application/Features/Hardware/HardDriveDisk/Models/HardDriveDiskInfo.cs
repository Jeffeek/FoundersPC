using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;

public class HardDriveDiskInfo : HardwareInfo
{
    public int? DiskFactorId { get; set; }
    public int? DiskConnectionInterfaceId { get; set; }
    public int? Volume { get; set; } = default!;
    public int? HeadSpeed { get; set; } = default!;
    public int? BufferSize { get; set; } = default!;
    public double? Noise { get; set; } = default!;
}