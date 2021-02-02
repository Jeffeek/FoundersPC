using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Application
{
    public class ProducerInsertDto
    {
	    [MinLength(2)]
	    [MaxLength(20)]
	    [DataType(DataType.Text)]
	    public string ShortName { get; set; }

	    [MinLength(3)]
	    [MaxLength(100)]
	    [DataType(DataType.Text)]
	    [Required]
		public string FullName { get; set; }

	    [MaxLength(50)]
	    [MinLength(3)]
	    [DataType(DataType.Text)]
		[Required]
	    public string Country { get; set; }

	    [MaxLength(100)]
	    [MinLength(5)]
	    [DataType(DataType.Url)]
		public string Website { get; set; }

	    [DataType(DataType.Date)]
		public DateTime? FoundationDate { get; set; }
	}
}
