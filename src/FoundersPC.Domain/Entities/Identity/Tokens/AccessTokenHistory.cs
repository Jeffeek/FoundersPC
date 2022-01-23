using System;
using FoundersPC.Domain.Common;
using FoundersPC.Domain.Entities.Identity.Users;

namespace FoundersPC.Domain.Entities.Identity.Tokens;

public class AccessTokenHistory : IIdentityItem
{
    public int Id { get; set; }
    public int AccessTokenId { get; set; }
    public DateTime RequestDate { get; set; }
    public int RequestUserId { get; set; }

    public AccessToken AccessToken { get; set; } = default!;
    public ApplicationUser RequestUser { get; set; } = default!;
}