#region Using namespaces

using System;
using FoundersPC.API.Domain.Common;
using FoundersPC.API.Domain.Entities.Identity.Users;

#endregion

namespace FoundersPC.API.Domain.Entities.Identity.Logging
{
    public class UserEntranceLog : IdentityItem, IEquatable<UserEntranceLog>
    {
        public ApplicationUser ApplicationUser { get; set; } = default!;

        public int UserId { get; set; }

        public DateTime Entrance { get; set; }

        public bool Equals(UserEntranceLog other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return UserId == other.UserId
                   && Entrance.Equals(other.Entrance);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((UserEntranceLog)obj);
        }

        public override int GetHashCode() => HashCode.Combine(UserId, Entrance);
    }
}