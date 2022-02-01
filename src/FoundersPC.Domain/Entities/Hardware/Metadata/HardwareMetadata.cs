using FoundersPC.Domain.Common;

namespace FoundersPC.Domain.Entities.Hardware.Metadata;

public abstract class HardwareMetadata : IIdentityItem
{
    public int Id { get; set; }
    public int ProducerId { get; set; }
    public string Title { get; set; } = default!;
    public int HardwareTypeId { get; set; }
    public HardwareType HardwareType { get; set; } = default!;
    public Producer Producer { get; set; } = default!;
    public Hardware Hardware { get; set; } = default!;
}