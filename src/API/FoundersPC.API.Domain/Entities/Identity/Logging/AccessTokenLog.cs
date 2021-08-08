#region Using namespaces

using System;
using FoundersPC.API.Domain.Common;
using FoundersPC.API.Domain.Entities.Identity.Tokens;

#endregion

namespace FoundersPC.API.Domain.Entities.Identity.Logging
{
    public class AccessTokenLog : IdentityItem, IEquatable<AccessTokenLog>
    {
        public AccessToken AccessToken { get; set; } = default!;

        public int ApiAccessUserTokenId { get; set; }

        public DateTime RequestDateTime { get; set; }

        public bool Equals(AccessTokenLog other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return ApiAccessUserTokenId == other.ApiAccessUserTokenId
                   && RequestDateTime.Equals(other.RequestDateTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((AccessTokenLog)obj);
        }

        public override int GetHashCode() => HashCode.Combine(ApiAccessUserTokenId, RequestDateTime);
    }
}