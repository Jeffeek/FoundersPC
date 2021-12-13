#region Using namespaces

using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class SolidStateDrive : Hardware
{
    public SolidStateDriveMetadata Metadata { get; set; } = default!;
}