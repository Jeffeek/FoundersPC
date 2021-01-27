using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoundersPC.Services.Models
{
    public class CrystalSerial
    {
	    /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(ChipProducerId)}: {ChipProducerId}, {nameof(ChipProducer)}: {ChipProducer}, {nameof(Microarchitecture)}: {Microarchitecture}";

	    public CrystalSerial(string name, int? chipProducerId, ChipProducer chipProducer, string microarchitecture)
	    {
		    Name = name;
		    ChipProducerId = chipProducerId;
		    ChipProducer = chipProducer;
		    Microarchitecture = microarchitecture;
	    }

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
        [MaxLength(10)]
        [MinLength(1)]
        [Column("ChipProducerId")]
        public int? ChipProducerId { get; set; }
        
        [ForeignKey(nameof(ChipProducerId))]
        public ChipProducer ChipProducer { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [MinLength(3)]
        [Column("Microarchitecture")]
        public string Microarchitecture { get; set; }
    }
}
