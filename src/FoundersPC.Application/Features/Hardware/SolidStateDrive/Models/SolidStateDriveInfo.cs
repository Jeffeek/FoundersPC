using FoundersPC.Application.Features.Hardware.Models;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;

public class SolidStateDriveInfo : HardwareInfo
{
    public int? DiskFactorId { get; set; }
    public int? DiskConnectionInterfaceId { get; set; }
    public int? Volume { get; set; }
    public string? MicroScheme { get; set; }
    public double? SequentialRead { get; set; }
    public double? SequentialRecording { get; set; }
}