#region Using namespaces

using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class HardDriveDisk : Hardware
{
    public HardDriveDiskMetadata Metadata { get; set; } = default!;
}