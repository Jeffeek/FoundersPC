#region Using namespaces

using System;
using System.Collections.Generic;
using FoundersPC.Domain.Entities.Identity.Tokens;
using Microsoft.AspNetCore.Identity;

#endregion

namespace FoundersPC.Domain.Entities.Identity.Users;

public class ApplicationUser : IdentityUser<int>, IEquatable<ApplicationUser>
{
    public string Login { get; set; } = default!;

    public DateTime RegistrationDate { get; set; } = default!;

    public int RoleId { get; set; } = default!;

    public bool IsActive { get; set; } = default!;

    public bool IsBlocked { get; set; } = default!;

    public bool SendMessageOnEntrance { get; set; } = default!;

    public bool SendMessageOnApiRequest { get; set; } = default!;

    public ApplicationRole ApplicationRole { get; set; } = default!;

    public ICollection<AccessToken> Tokens { get; set; } = default!;

    public bool Equals(ApplicationUser? other) => Email == other?.Email;
}