#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
	public class Motherboard : EquipmentEntityBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Text)]
		[MaxLength(10)]
		[MinLength(3)]
		[Column("Socket")]
		[Required]
		public string Socket { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Factor")]
		[Required]
		public double Factor { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("RAMSupport")]
		[MinLength(5)]
		[MaxLength(6)]
		[Required]
		public string RAMSupport { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("RAMSlots")]
		[Required]
		public int RAMSlots { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("RAMMode")]
		[MinLength(2)]
		[MaxLength(2)]
		[Required]
		public string RAMMode { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SLI_Crossfire")]
		[Required]
		public bool SLIOrCrossfire { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("AudioSupport")]
		[MinLength(3)]
		[MaxLength(20)]
		[Required]
		public string AudioSupport { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("WiFiSupport")]
		[Required]
		public bool WiFiSupport { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("PS2Support")]
		[Required]
		public bool PS2Support { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("M2SlotsCount")]
		[Required]
		public int M2SlotsCount { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("PCIExpressVersion")]
		[DataType(DataType.Text)]
		[MinLength(3)]
		[MaxLength(12)]
		[Required]
		public string PCIExpressVersion { get; set; }
	}
}