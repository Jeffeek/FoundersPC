#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Domain.Entities
{
	public abstract class BaseEntity
	{
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }
	}
}