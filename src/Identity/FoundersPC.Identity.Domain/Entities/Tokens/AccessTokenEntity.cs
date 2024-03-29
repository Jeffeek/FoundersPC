﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.IdentityEntities.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Domain.Entities.Tokens
{
    [Index(nameof(Id))]
    public class AccessTokenEntity : IdentityItem, IEquatable<AccessTokenEntity>
    {
        [Required]
        [MaxLength(88)]
        [MinLength(88)]
        public string HashedToken { get; set; }

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

        public ICollection<AccessTokenLog> UsagesLogs { get; set; }

        public bool Equals(AccessTokenEntity other)
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

            return obj.GetType() == GetType() && Equals((AccessTokenEntity)obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(UserId,
                             StartEvaluationDate,
                             ExpirationDate,
                             IsBlocked);
    }
}