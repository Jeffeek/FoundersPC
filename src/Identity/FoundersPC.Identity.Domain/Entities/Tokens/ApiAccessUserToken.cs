using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.ApplicationShared.Identity;
using FoundersPC.Identity.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Domain.Entities.Tokens
{
    [Index(nameof(Id))]
    public class ApiAccessUserToken : IdentityItem, IEquatable<ApiAccessUserToken>
    {
        [ForeignKey(nameof(ApiAccessTokenId))]
        public ApiAccessToken Token { get; set; }

        [Required]
        public int ApiAccessTokenId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime StartEvaluationDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [DefaultValue(true)]
        [Required]
        public bool IsBlocked { get; set; }

        public bool Equals(ApiAccessUserToken other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return ApiAccessTokenId == other.ApiAccessTokenId
                   && UserId == other.UserId
                   && StartEvaluationDate.Equals(other.StartEvaluationDate)
                   && ExpirationDate.Equals(other.ExpirationDate)
                   && IsBlocked == other.IsBlocked;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((ApiAccessUserToken)obj);
        }

        public override int GetHashCode() => HashCode.Combine(ApiAccessTokenId,
                                                              UserId,
                                                              StartEvaluationDate,
                                                              ExpirationDate,
                                                              IsBlocked);
    }
}
