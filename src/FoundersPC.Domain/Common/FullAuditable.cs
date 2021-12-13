#region Using namespaces

using System;
using FoundersPC.Domain.Entities.Identity.Users;

#endregion

namespace FoundersPC.Domain.Common;

public abstract class FullAuditable
{
    public DateTime Created { get; set; }
    public int CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; } = default!;

    public DateTime LastModified { get; set; }
    public int LastModifiedById { get; set; }
    public ApplicationUser LastModifiedBy { get; set; } = default!;

    public bool IsDeleted { get; set; }
    public int? DeletedById { get; set; }
    public ApplicationUser? DeletedBy { get; set; }
    public DateTime? Deleted { get; set; }
}