using System;
using FoundersPC.Domain.Entities.Identity.Users;

namespace FoundersPC.Domain.Common;

public class Auditable
{
    public DateTime Created { get; set; }
    public int CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; } = default!;

    public DateTime LastModified { get; set; }
    public int LastModifiedById { get; set; }
    public ApplicationUser LastModifiedBy { get; set; } = default!;
}