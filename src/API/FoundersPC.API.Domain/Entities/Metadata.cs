using FoundersPC.API.Domain.Common;
using FoundersPC.API.Domain.Enums;

namespace FoundersPC.API.Domain.Entities
{
    public abstract class Metadata : IdentityItem
    {
        public int MetadataId { get; set; }

        public MetadataType MetadataType { get; set; }

        public string? Name { get; set; } = default!;
    }
}