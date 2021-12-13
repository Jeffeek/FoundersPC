#region Using namespaces

using FoundersPC.Domain.Common;
using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public abstract class Hardware : FullAuditable, IIdentityItem
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }
    public HardwareType HardwareType { get; set; } = default!;
    public HardwareMetadata BaseMetadata { get; set; } = default!;
}