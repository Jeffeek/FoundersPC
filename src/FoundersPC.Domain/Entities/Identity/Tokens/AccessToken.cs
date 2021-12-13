#region Using namespaces

using System;
using FoundersPC.Domain.Common;
using FoundersPC.Domain.Entities.Identity.Users;

#endregion

namespace FoundersPC.Domain.Entities.Identity.Tokens;

public class AccessToken : IIdentityItem, IEquatable<AccessToken>
{
    public int Id { get; set; }
    public string Token { get; set; } = default!;
    public ApplicationUser ApplicationUser { get; set; } = default!;
    public int UserId { get; set; }
    public DateTime StartEvaluationDate { get; set; } = default!;
    public DateTime ExpirationDate { get; set; } = default!;
    public bool IsBlocked { get; set; } = default!;

    public bool Equals(AccessToken? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return UserId == other.UserId
               && StartEvaluationDate.Equals(other.StartEvaluationDate)
               && ExpirationDate.Equals(other.ExpirationDate)
               && IsBlocked == other.IsBlocked;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((AccessToken)obj);
    }

    public override int GetHashCode() =>
        HashCode.Combine(UserId,
                         StartEvaluationDate,
                         ExpirationDate,
                         IsBlocked);

}