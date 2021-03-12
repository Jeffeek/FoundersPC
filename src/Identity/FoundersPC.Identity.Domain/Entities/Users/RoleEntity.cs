#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoundersPC.Identity.Domain.Common.Interfaces;
using FoundersPC.RepositoryShared.Identity;

#endregion

namespace FoundersPC.Identity.Domain.Entities.Users
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