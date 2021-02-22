using System;
using System.ComponentModel.DataAnnotations;
using FoundersPC.ApplicationShared.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Domain.Entities
{
    [Index(nameof(Id))]
    public class ApiAccessToken : IdentityItem, IEquatable<ApiAccessToken>
    {
        [MinLength(88)]
        [MaxLength(88)]
        [Required]
        public string HashedToken { get; set; }

        public bool Equals(ApiAccessToken other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return HashedToken == other.HashedToken;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((ApiAccessToken)obj);
        }

        public override int GetHashCode() => (HashedToken != null ? HashedToken.GetHashCode() : 0);
    }
}
