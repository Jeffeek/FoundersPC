#region Using derectives

using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Services.Models.Hardware.Memory
{
	public class HDD : DriveBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("HeadSpeed")]
		public int HeadSpeed { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("BufferSize")]
		public int BufferSize { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Noise")]
		public int Noise { get; set; }
	}
}