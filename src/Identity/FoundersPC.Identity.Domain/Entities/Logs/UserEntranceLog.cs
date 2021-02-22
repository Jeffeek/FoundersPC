using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.ApplicationShared.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Domain.Entities
{
    [Index(nameof(Id))]
    public class UserEntranceLog : IdentityItem, IEquatable<UserEntranceLog>
    {
        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Entrance { get; set; }

        public bool Equals(UserEntranceLog other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return UserId == other.UserId
                   && Entrance.Equals(other.Entrance);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((UserEntranceLog)obj);
        }

        public override int GetHashCode() => HashCode.Combine(UserId, Entrance);
    }
}
