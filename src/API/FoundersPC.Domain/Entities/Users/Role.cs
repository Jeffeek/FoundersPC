using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Domain.Entities.Users
{
	[Index(nameof(Id))]
	public class Role : IdentityItem
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Text)]
		[Required]
		public string RoleTitle { get; set; }

		public ICollection<User> Users { get; set; }
	}
}