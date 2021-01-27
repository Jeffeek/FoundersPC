using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
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
