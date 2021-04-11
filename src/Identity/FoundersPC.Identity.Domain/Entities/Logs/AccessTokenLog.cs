#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.IdentityEntities.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Domain.Entities.Logs
{
    [Index(nameof(Id))]
    public class AccessTokenLog : IdentityItem, IEquatable<AccessTokenLog>
    {
        [ForeignKey(nameof(ApiAccessUsersTokenId))]
        public ApiAccessUserToken ApiAccessToken { get; set; }

        [Required]
        public int ApiAccessUsersTokenId { get; set; }

        [Required]
        public DateTime RequestDateTime { get; set; }

        public bool Equals(AccessTokenLog other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return ApiAccessUsersTokenId == other.ApiAccessUsersTokenId
                   && RequestDateTime.Equals(other.RequestDateTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((AccessTokenLog)obj);
        }

        public override int GetHashCode() => HashCode.Combine(ApiAccessUsersTokenId, RequestDateTime);
    }
}