using System;

namespace FoundersPC.Application.UsersIdentity
{
	public class UserReadDto
	{
		public string Email { get; set; }

		public string PasswordHash { get; set; }

		public DateTime CreatedAt { get; set; }

		public bool IsEmailConfirmed { get; set; }

		public int RoleId { get; set; }

		public RoleReadDto Role { get; set; }

		public bool IsActive { get; set; }
	}
}
