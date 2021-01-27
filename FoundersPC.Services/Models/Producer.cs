using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
	public class Producer
    {
	    [Key]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
		[Column("Website")]
		public string Website { get; set; }
	    
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Date)]
		[Column("FoundationDate")]
		public DateTime? FoundationDate { get; set; }
    }
}
