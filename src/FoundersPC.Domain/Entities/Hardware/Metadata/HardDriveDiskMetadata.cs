using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class HardDriveDiskMetadata : HardwareMetadata
{
    public int? DiskFactorId { get; set; }
    public int? DiskConnectionInterfaceId { get; set; }
    public int? Volume { get; set; } = default!;
    public int? HeadSpeed { get; set; } = default!;
    public int? BufferSize { get; set; } = default!;
    public double? Noise { get; set; } = default!;
    public DiskFactor? Factor { get; set; } = default!;
    public DiskConnectionInterface? DiskConnectionInterface { get; set; } = default!;
    public HardDriveDisk HardDriveDisk { get; set; } = default!;
}