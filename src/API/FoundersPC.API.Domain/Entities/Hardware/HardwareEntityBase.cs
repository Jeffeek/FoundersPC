#region Using namespaces

using FoundersPC.API.Domain.Common;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public abstract class HardwareEntityBase : IdentityItem
    {
        public int ProducerId { get; set; }

        public Producer Producer { get; set; } = default!;

        public string? Title { get; set; } = default!;
    }
}