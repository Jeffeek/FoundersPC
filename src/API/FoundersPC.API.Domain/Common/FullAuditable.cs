using System;
using FoundersPC.API.Domain.Entities.Identity.Users;

namespace FoundersPC.API.Domain.Common
{
    public abstract class FullAuditable : IdentityItem
    {
        public DateTime Created { get; set; } = default!;

        public int CreatedById { get; set; }

        public ApplicationUser CreatedBy { get; set; } = default!;

        public DateTime LastModified { get; set; } = default!;

        public int LastModifiedById { get; set; } = default!;

        public ApplicationUser LastModifiedBy { get; set; } = default!;

        public bool IsDeleted { get; set; }

        public int? DeletedById { get; set; } = default!;

        public ApplicationUser DeletedBy { get; set; } = default!;

        public DateTime? Deleted { get; set; } = default!;
    }
}