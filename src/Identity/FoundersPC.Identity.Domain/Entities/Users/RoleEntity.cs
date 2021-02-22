#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoundersPC.ApplicationShared.Identity;
using FoundersPC.Identity.Domain.Common.Interfaces;

#endregion

namespace FoundersPC.Identity.Domain.Entities
{
    public class RoleEntity : IdentityItem, IRole, IEquatable<RoleEntity>
    {
        public ICollection<UserEntity> Users { get; set; }

        public bool Equals(RoleEntity other) => RoleTitle == other?.RoleTitle;

        [DataType(DataType.Text)]
        [Required]
        public string RoleTitle { get; set; }
    }
}