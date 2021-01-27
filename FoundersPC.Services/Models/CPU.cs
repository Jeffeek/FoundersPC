using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
    public class CPU
    {
	    /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}," +
	                                         $" {nameof(TechProcess)}: {TechProcess}," +
	                                         $" {nameof(Series)}: {Series}," +
	                                         $" {nameof(MaxRamSpeed)}: {MaxRamSpeed}," +
	                                         $" {nameof(CrystalSerialId)}: {CrystalSerialId}," +
	                                         $" {nameof(CrystalSerial)}: {CrystalSerial}," +
	                                         $" {nameof(Socket)}: {Socket}," +
	                                         $" {nameof(Cores)}: {Cores}," +
	                                         $" {nameof(Frequency)}: {Frequency}," +
	                                         $" {nameof(TurboBoostFrequency)}: {TurboBoostFrequency}," +
	                                         $" {nameof(L3Cache)}: {L3Cache}," +
	                                         $" {nameof(IntegratedGraphics)}: {IntegratedGraphics}";

	    [Key]
	    [Column("Id")]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public int Id { get; set; }

	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TechProcess")]
        public int TechProcess { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [MinLength(3)]
        [Column("Series")]
        public string Series { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MaxRamSpeed")]
        public int MaxRamSpeed { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CrystalSerialId")]
        public int CrystalSerialId { get; set; }
        
        [ForeignKey(nameof(CrystalSerialId))]
        public CrystalSerial CrystalSerial { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [MinLength(3)]
        [Column("Socket")]
        public string Socket { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Cores")]
        public int Cores { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Frequency")]
        public int Frequency { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TurboBoostFrequency")]
        public int TurboBoostFrequency { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("L3Cache")]
        public int L3Cache { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IntegratedGraphics")]
        public bool IntegratedGraphics { get; set; }
    }
}
