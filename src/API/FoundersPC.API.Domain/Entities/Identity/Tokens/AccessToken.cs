#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Common;
using FoundersPC.API.Domain.Entities.Identity.Logging;
using FoundersPC.API.Domain.Entities.Identity.Users;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Domain.Entities.Identity.Tokens
{
    public class AccessToken : IdentityItem, IEquatable<AccessToken>
    {
        public string Token { get; set; } = default!;

        public ApplicationUser ApplicationUser { get; set; } = default!;

        public int UserId { get; set; }

        public DateTime StartEvaluationDate { get; set; } = default!;

        public DateTime ExpirationDate { get; set; } = default!;

        public bool IsBlocked { get; set; } = default!;

        public ICollection<AccessTokenLog> UsageLogs { get; set; } = default!;

        public bool Equals(AccessToken other)
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

        public override bool Equals(object obj)
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
}