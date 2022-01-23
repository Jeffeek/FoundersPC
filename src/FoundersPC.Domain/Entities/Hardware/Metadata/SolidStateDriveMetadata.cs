using FoundersPC.Domain.Entities.Metadata;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public class SolidStateDriveMetadata : HardwareMetadata
{
    public int? DiskFactorId { get; set; } = default!;
    public int? DiskConnectionInterfaceId { get; set; } = default!;
    public int? Volume { get; set; } = default!;
    public string? MicroScheme { get; set; } = default!;
    public double? SequentialRead { get; set; } = default!;
    public double? SequentialRecording { get; set; } = default!;
    public DiskFactor? DiskFactor { get; set; } = default!;
    public DiskConnectionInterface? DiskConnectionInterface { get; set; } = default!;
    public SolidStateDrive SolidStateDrive { get; set; } = default!;
}