using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersPC.Services.Models.Memory
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
