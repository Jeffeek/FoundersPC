using System;

namespace FoundersPC.API.Domain.Common
{
    public abstract class FullAuditable : IdentityItem
    {
        public DateTime Created { get; set; } = default!;

        public int CreatedById { get; set; }

        public DateTime LastModified { get; set; } = default!;

        public int LastModifiedById { get; set; } = default!;

        public bool IsDeleted { get; set; }

        public int? DeletedById { get; set; } = default!;

        public DateTime? Deleted { get; set; } = default!;
    }
}