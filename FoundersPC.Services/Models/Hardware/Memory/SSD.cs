#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Services.Models.Hardware.Memory
{
	public class SSD : DriveBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MicroScheme")]
		[MinLength(3)]
		[MaxLength(50)]
		[Required]
		public string MicroScheme { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SequentialRead")]
		[Required]
		public int SequentialRead { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SequentialRecording")]
		[Required]
		public int SequentialRecording { get; set; }
	}
}