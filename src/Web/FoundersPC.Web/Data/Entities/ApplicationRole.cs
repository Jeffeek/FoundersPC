#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.ApplicationShared.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Web.Data.Entities
{
    [Index(nameof(Id))]
    public class ApplicationRole : IdentityItem, IEquatable<ApplicationRole>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [Required]
        public string RoleTitle { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public bool Equals(ApplicationRole other)
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

            return Equals((ApplicationRole)obj);
        }

        public override int GetHashCode() => RoleTitle != null ? RoleTitle.GetHashCode() : 0;
    }
}