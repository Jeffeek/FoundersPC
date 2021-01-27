using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
    [Index(nameof(Id))]
    public class ChipProducer
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [Column("Id")]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [MinLength(3)]
        [Column("Name")]
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        [MinLength(5)]
        [DataType(DataType.Text)]
        [Column("Country")]
        public string Country { get; set; }

        public ICollection<CrystalSerial> CrystalSerials { get; set; }
        public ICollection<SSD> SSDs { get; set; }

        /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Country)}: {Country}";
    }
}
