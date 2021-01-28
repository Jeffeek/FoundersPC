#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Models.Memory
{
	[Index(nameof(Id))]
	public abstract class DriveBase : EquipmentEntityBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Factor")]
		public double Factor { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Interface")]
		[MinLength(3)]
		[MaxLength(20)]
		public string Interface { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Volume")]
		public int Volume { get; set; }
	}
}