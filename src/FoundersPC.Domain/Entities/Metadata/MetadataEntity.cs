#region Using namespaces

using FoundersPC.Domain.Common;

#endregion

namespace FoundersPC.Domain.Entities.Metadata;

public abstract class MetadataEntity : FullAuditable, IIdentityItem
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}