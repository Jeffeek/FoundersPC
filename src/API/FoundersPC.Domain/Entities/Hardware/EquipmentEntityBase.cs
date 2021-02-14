#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Domain.Entities.Hardware
{
	public abstract class EquipmentEntityBase : IdentityItem
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ProducerId")]
		[Required]
		public int ProducerId { get; set; }

		[ForeignKey(nameof(ProducerId))]
		public Producer Producer { get; set; }

		[MaxLength(100)]
		[MinLength(0)]
		[DataType(DataType.Text)]
		[Column("Title")]
		[Required]
		public string Title { get; set; }
	}
}