using FoundersPC.Application.Features.Client.Hardware.Models;

namespace FoundersPC.Application.Features.Client.Hardware.SolidStateDrive.Models;

public class ClientSolidStateDriveInfo : ClientHardwareInfo
{
    public string? DiskFactor { get; set; }
    public string? DiskConnectionInterface { get; set; }
    public int? Volume { get; set; }
    public string? MicroScheme { get; set; }
    public double? SequentialRead { get; set; }
    public double? SequentialRecording { get; set; }
}