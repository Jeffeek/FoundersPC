using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersPC.Services.Models.Hardware.VideoCard
{
    public class VideoCardCore
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(30)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Title")]
        public string Title { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TechProcess")]
        [Required]
        public int TechProcess { get; set; }

        [MinLength(5)]
        [MaxLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MaxResolution")]
        [Required]
        public string MaxResolution { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MonitorsSupport")]
        [Required]
        public int MonitorsSupport { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Interface")]
        [MinLength(5)]
        [MaxLength(30)]
        [DataType(DataType.Text)]
        [Required]
        public string Interface { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Frequency")]
        [MinLength(3)]
        [MaxLength(5)]
        [Required]
        public int Frequency { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("DirectX_Version")]
        [MinLength(1)]
        [MaxLength(3)]
        public int DirectX { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SLI_Crossfire")]
        [Required]
        public bool SLIOrCrossfire { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [Column("ArchitectureTitle")]
        [Required]
        public string ArchitectureTitle { get; set; }

        public ICollection<GPU> VideoCards { get; set; }
    }
}
