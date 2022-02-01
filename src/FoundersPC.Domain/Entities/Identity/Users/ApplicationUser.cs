#region Using namespaces

using System;
using System.Collections.Generic;
using FoundersPC.Domain.Common;
using FoundersPC.Domain.Entities.Identity.Tokens;
using Microsoft.AspNetCore.Identity;

#endregion

namespace FoundersPC.Domain.Entities.Identity.Users;

public class ApplicationUser : IdentityUser<int>, IIdentityItem
{
    public string Login { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    public int RoleId { get; set; }
    public bool IsBlocked { get; set; }
    public ApplicationRole ApplicationRole { get; set; } = default!;
    public ICollection<AccessToken> Tokens { get; set; } = default!;
    public ICollection<AccessTokenHistory> TokensHistories { get; set; } = default!;
}