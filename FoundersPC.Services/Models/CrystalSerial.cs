using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
    public class CrystalSerial
    {
	    /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}," +
	                                         $" {nameof(Name)}: {Name}," +
	                                         $" {nameof(ChipProducerId)}: {ChipProducerId}," +
	                                         $" {nameof(ChipProducer)}: {ChipProducer}," +
	                                         $" {nameof(MicroArchitecture)}: {MicroArchitecture}";

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
        [Column("ChipProducerId")]
        public int ChipProducerId { get; set; }
        
        [ForeignKey(nameof(ChipProducerId))]
        public ChipProducer ChipProducer { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [MinLength(3)]
        [Column("MicroArchitecture")]
        public string MicroArchitecture { get; set; }

        public ICollection<GPU> GPUs { get; set; }
        public ICollection<CPU> CPUs { get; set; }
    }
}
