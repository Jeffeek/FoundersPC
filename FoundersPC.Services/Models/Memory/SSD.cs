#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Services.Models.Memory
{
	public class SSD : DriveBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MicroScheme")]
		[MinLength(3)]
		[MaxLength(50)]
		public string MicroScheme { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SequentialRead")]
		public int SequentialRead { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SequentialRecording")]
		public int SequentialRecording { get; set; }
	}
}