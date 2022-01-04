#region Using namespaces

using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class PowerSupply : Hardware
{
    public PowerSupplyMetadata Metadata { get; set; } = default!;
}