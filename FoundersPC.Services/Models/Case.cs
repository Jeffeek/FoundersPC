using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
    [Index(nameof(Id))]
    public class Case : EquipmentEntityBase
    {
        [Column("Type")]
        [MinLength(40)]
        [MaxLength(3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        public string Type { get; set; }

        [Column("MaxMotherboardSize")]
        [MinLength(3)]
        [MaxLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        public string MaxMotherboardSize { get; set; }

        [Column("Material")]
        [MinLength(3)]
        [MaxLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        public string Material { get; set; }

        [Column("WindowMaterial")]
        [MinLength(3)]
        [MaxLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        public string WindowMaterial { get; set; }

        [Column("TransparentWindow")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool TransparentWindow { get; set; }

        [Column("Color")]
        [MinLength(2)]
        [MaxLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        public string Color { get; set; }
    }
}
