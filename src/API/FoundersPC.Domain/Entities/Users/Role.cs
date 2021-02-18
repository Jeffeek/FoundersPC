#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Domain.Entities.Users
{
    [Index(nameof(Id))]
    public class Role : IdentityItem, IEquatable<Role>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [Required]
        public string RoleTitle { get; set; }

        public ICollection<User> Users { get; set; }

        public bool Equals(Role other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return RoleTitle == other.RoleTitle;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Role)obj);
        }

        public override int GetHashCode() => RoleTitle != null ? RoleTitle.GetHashCode() : 0;
    }
}