#region Using namespaces

using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class Processor : Hardware
{
    public ProcessorMetadata Metadata { get; set; } = default!;
}