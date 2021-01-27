using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
    public class Producer
    {
	    [Key]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [MinLength(1)]
	    [MaxLength(10)]
	    [Column("Id")]
	    public int Id { get; set; }
	    
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
		[MinLength(2)]
		[MaxLength(20)]
	    [DataType(DataType.Text)]
		[Column("ShortName")]
	    public string ShortName { get; set; }
	    
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
	    [MinLength(3)]
		[MaxLength(100)]
	    [DataType(DataType.Text)]
	    [Column("FullName")]
		public string FullName { get; set; }
	    
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[MaxLength(50)]
		[MinLength(3)]
		[DataType(DataType.Text)]
		[Column("Country")]
	    public string Country { get; set; }
		
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
		[MaxLength(50)]
		[MinLength(5)]
		[DataType(DataType.Url)]
		[Column("Country")]
		public string Website { get; set; }
	    
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Date)]
		[Column("FoundationDate")]
		public DateTime? FoundationDate { get; set; }

	    public Producer(string shortName, string fullName, string country, string website, DateTime? foundationDate)
	    {
		    ShortName = shortName;
		    FullName = fullName;
		    Country = country;
		    Website = website;
		    FoundationDate = foundationDate;
	    }
	    
	    /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(ShortName)}: {ShortName}, {nameof(FullName)}: {FullName}, {nameof(Country)}: {Country}, {nameof(Website)}: {Website}, {nameof(FoundationDate)}: {FoundationDate}";
    }
}
