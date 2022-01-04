using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;

public class SolidStateDriveInfo : HardwareInfo
{
    public int? DiskFactorId { get; set; } = default!;
    public int? DiskConnectionInterfaceId { get; set; } = default!;
    public int? Volume { get; set; } = default!;
    public string? MicroScheme { get; set; } = default!;
    public double? SequentialRead { get; set; } = default!;
    public double? SequentialRecording { get; set; } = default!;
}