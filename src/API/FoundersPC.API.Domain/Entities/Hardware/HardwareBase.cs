#region Using namespaces

using FoundersPC.API.Domain.Common;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public abstract class HardwareBase : IdentityItem
    {
        public int ProducerId { get; set; }

        public Producer Producer { get; set; } = default!;

        public string Title { get; set; } = default!;

        public Enums.HardwareType Type { get; set; }
    }
}