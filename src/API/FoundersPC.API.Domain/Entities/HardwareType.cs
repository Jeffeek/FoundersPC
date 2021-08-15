using FoundersPC.API.Domain.Common;

namespace FoundersPC.API.Domain.Entities
{
    public class HardwareType : IdentityItem
    {
        public string? Name { get; set; }

        public Enums.HardwareType Type { get; set; }
    }
}