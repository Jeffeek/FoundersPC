﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Identity.Domain.Common.Interfaces;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.IdentityEntities.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Domain.Entities.Users
{
    [Index(nameof(Email), IsUnique = true, Name = "ix_users_email")]
    [Index(nameof(Id), IsUnique = true, Name = "ix_users_id")]
    public class UserEntity : IdentityItem, IUser, IEquatable<UserEntity>
    {
        [MaxLength(30)]
        [MinLength(5)]
        [DataType(DataType.Text)]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool SendMessageOnEntrance { get; set; }

        [Required]
        public bool SendMessageOnApiRequest { get; set; }

        [ForeignKey(nameof(RoleId))]
        public RoleEntity Role { get; set; }

        public ICollection<AccessTokenEntity> Tokens { get; set; }

        public ICollection<UserEntranceLog> Entrances { get; set; }

        public bool Equals(UserEntity other) => Email == other?.Email;

        [MaxLength(128)]
        [MinLength(3)]
        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(128)]
        [MinLength(128)]
        [Required]
        [DataType(DataType.Password)]
        public string HashedPassword { get; set; }
    }
}