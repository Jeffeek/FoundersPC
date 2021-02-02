#region Using derectives

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Domain.Entities.Hardware
{
	public abstract class EquipmentEntityBase : BaseEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ProducerId")]
		[Required]
		public int ProducerId { get; set; }

		[ForeignKey(nameof(ProducerId))]
		public Producer Producer { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MarketLaunch")]
		[DataType(DataType.Date)]
		public DateTime? MarketLaunch { get; set; }
	}
}