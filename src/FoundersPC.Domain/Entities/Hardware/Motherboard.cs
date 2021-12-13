#region Using namespaces

using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class Motherboard : Hardware
{
    public MotherboardMetadata Metadata { get; set; } = default!;
}