#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Domain.Entities
{
	public abstract class IdentityItem
	{
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		[Key]
		public int Id { get; set; }
	}
}