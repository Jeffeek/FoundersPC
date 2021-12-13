#region Using namespaces

using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class RandomAccessMemory : Hardware
{
    public RandomAccessMemoryMetadata Metadata { get; set; } = default!;
}