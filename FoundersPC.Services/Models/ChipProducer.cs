using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersPC.Services.Models
{
    public class ChipProducer
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(10)]
	    [MinLength(1)]
        [Column("Id")]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        [MinLength(3)]
        [Column("Name")]
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        [MinLength(5)]
        [DataType(DataType.Text)]
        [Column("Country")]
        public string Country { get; set; }

	    public ChipProducer(string name, string country)
	    {
		    Name = name;
		    Country = country;
	    }

	    /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Country)}: {Country}";
    }
}
