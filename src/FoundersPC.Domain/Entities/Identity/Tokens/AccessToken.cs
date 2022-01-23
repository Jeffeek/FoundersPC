#region Using namespaces

using System;
using System.Collections.Generic;
using FoundersPC.Domain.Common;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Domain.Enums;

#endregion

namespace FoundersPC.Domain.Entities.Identity.Tokens;

public class AccessToken : IIdentityItem
{
    public int Id { get; set; }
    public string Token { get; set; } = default!;
    public int UserId { get; set; }
    public TokenPackageType Type { get; set; }
    public DateTime StartEvaluationDate { get; set; } = default!;
    public DateTime ExpirationDate { get; set; } = default!;
    public bool IsBlocked { get; set; } = default!;
    public ApplicationUser ApplicationUser { get; set; } = default!;
    public List<AccessTokenHistory> History { get; set; } = default!;
}