#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Domain.Common.Interfaces;
using FoundersPC.Domain.Entities.Hardware;

#endregion

namespace FoundersPC.Domain.Common.Base
{
	public abstract class HardwareEntityBase : IHardware
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

		[Column("Id")]
		[Required]
		[Key]
        public int Id { get; set; }
    }
}