using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoundersPC.Services.Models
{
    public class CPU
    {
	    /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(ProducerId)}: {ProducerId}, {nameof(ChipProducer)}: {ChipProducer}, {nameof(TechProcess)}: {TechProcess}, {nameof(Series)}: {Series}, {nameof(MaxRamSpeed)}: {MaxRamSpeed}, {nameof(CrystalSerialId)}: {CrystalSerialId}, {nameof(CrystalSerial)}: {CrystalSerial}, {nameof(Socket)}: {Socket}, {nameof(Cores)}: {Cores}, {nameof(Frequency)}: {Frequency}, {nameof(TurboBoostFrequency)}: {TurboBoostFrequency}, {nameof(L3Cache)}: {L3Cache}, {nameof(IntegratedGraphics)}: {IntegratedGraphics}";

	    public CPU(int? producerId, ChipProducer chipProducer, int techProcess, string series, int maxRamSpeed,
	               int? crystalSerialId, CrystalSerial crystalSerial, string socket, int cores, int frequency,
	               int turboBoostFrequency, int l3Cache, bool integratedGraphics)
	    {
		    ProducerId = producerId;
		    ChipProducer = chipProducer;
		    TechProcess = techProcess;
		    Series = series;
		    MaxRamSpeed = maxRamSpeed;
		    CrystalSerialId = crystalSerialId;
		    CrystalSerial = crystalSerial;
		    Socket = socket;
		    Cores = cores;
		    Frequency = frequency;
		    TurboBoostFrequency = turboBoostFrequency;
		    L3Cache = l3Cache;
		    IntegratedGraphics = integratedGraphics;
	    }

	    [Key]
	    [Column("Id")]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [MinLength(1)]
	    [MaxLength(10)]
	    public int Id { get; set; }
	    
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
	    [MaxLength(10)]
	    [MinLength(1)]
	    [Column("ProducerId")]
        public int? ProducerId { get; set; }
	    
        [ForeignKey(nameof(ProducerId))]
        public ChipProducer ChipProducer { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(2)]
        [MinLength(1)]
        [Column("TechProcess")]
        public int TechProcess { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        [MinLength(3)]
        [Column("Series")]
        public string Series { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(5)]
        [MinLength(3)]
        [Column("MaxRamSpeed")]
        public int MaxRamSpeed { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(10)]
        [MinLength(1)]
        [Column("CrystalSerialId")]
        public int? CrystalSerialId { get; set; }
        
        [ForeignKey(nameof(CrystalSerialId))]
        public CrystalSerial CrystalSerial { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(10)]
        [DataType(DataType.Text)]
        [MinLength(3)]
        [Column("Socket")]
        public string Socket { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(4)]
        [MinLength(1)]
        [Column("Cores")]
        public int Cores { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(5)]
        [MinLength(3)]
        [Column("Frequency")]
        public int Frequency { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(5)]
        [MinLength(3)]
        [Column("TurboBoostFrequency")]
        public int TurboBoostFrequency { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(6)]
        [MinLength(3)]
        [Column("L3Cache")]
        public int L3Cache { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IntegratedGraphics")]
        public bool IntegratedGraphics { get; set; }
    }
}
