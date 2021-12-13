#region Using namespaces

using System;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Domain.Entities.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class PowerSupply : Hardware
{
    public PowerSupplyMetadata Metadata { get; set; } = default!;
}