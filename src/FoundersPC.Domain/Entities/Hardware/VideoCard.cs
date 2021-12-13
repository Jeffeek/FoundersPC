#region Using namespaces

using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class VideoCard : Hardware
{
    public VideoCardMetadata Metadata { get; set; } = default!;
}