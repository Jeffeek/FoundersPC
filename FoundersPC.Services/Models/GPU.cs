using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoundersPC.Services.Models
{
    public class GPU
    {
	    /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}," +
	                                         $" {nameof(CrystalSerialId)}: {CrystalSerialId}," +
	                                         $" {nameof(CrystalSerial)}: {CrystalSerial}," +
	                                         $" {nameof(Interface)}: {Interface}," +
	                                         $" {nameof(ProducerId)}: {ProducerId}," +
	                                         $" {nameof(Producer)}: {Producer}," +
	                                         $" {nameof(GraphicProcessor)}: {GraphicProcessor}," +
	                                         $" {nameof(Frequency)}: {Frequency}," +
	                                         $" {nameof(VRAM)}: {VRAM}," +
	                                         $" {nameof(VRAMType)}: {VRAMType}," +
	                                         $" {nameof(VRAMFrequency)}: {VRAMFrequency}," +
	                                         $" {nameof(VRAMBusWidth)}: {VRAMBusWidth}," +
	                                         $" {nameof(DirectX)}: {DirectX}," +
	                                         $" {nameof(SLIOrCrossfire)}: {SLIOrCrossfire}," +
	                                         $" {nameof(AdditionalPower)}: {AdditionalPower}," +
	                                         $" {nameof(Width)}: {Width}, {nameof(VGA)}: {VGA}," +
	                                         $" {nameof(DVI)}: {DVI}," +
	                                         $" {nameof(HDMI)}: {HDMI}," +
	                                         $" {nameof(DisplayPort)}: {DisplayPort}";

	    public GPU(int? crystalSerialId, CrystalSerial crystalSerial, string @interface, int producerId,
	               Producer producer, string graphicProcessor, int frequency, int vram, string vramType,
	               int vramFrequency, int vramBusWidth, int directX, bool sliOrCrossfire, int additionalPower,
	               int width, int vga, int dvi, int hdmi, int displayPort)
	    {
		    CrystalSerialId = crystalSerialId;
		    CrystalSerial = crystalSerial;
		    Interface = @interface;
		    ProducerId = producerId;
		    Producer = producer;
		    GraphicProcessor = graphicProcessor;
		    Frequency = frequency;
		    VRAM = vram;
		    VRAMType = vramType;
		    VRAMFrequency = vramFrequency;
		    VRAMBusWidth = vramBusWidth;
		    DirectX = directX;
		    SLIOrCrossfire = sliOrCrossfire;
		    AdditionalPower = additionalPower;
		    Width = width;
		    VGA = vga;
		    DVI = dvi;
		    HDMI = hdmi;
		    DisplayPort = displayPort;
	    }

	    [Key]
	    [Column("Id")]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [MinLength(1)]
	    [MaxLength(10)]
        public int Id { get; set; }
	    
	    [Column("CrystalSerialId")]
	    [MinLength(0)]
	    [MaxLength(10)]
	    [DatabaseGenerated((DatabaseGeneratedOption.None))]
	    public int? CrystalSerialId { get; set; }
	    
	    [ForeignKey(nameof(CrystalSerialId))]
	    public CrystalSerial CrystalSerial { get; set; }
	    
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("Id")]
		[MinLength(5)]
		[MaxLength(30)]
		[DataType(DataType.Text)]
		public string Interface { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("ProducerId")]
		[MinLength(1)]
		[MaxLength(10)]
		public int ProducerId { get; set; }
		
		[ForeignKey(nameof(ProducerId))]
	    public Producer Producer { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("GraphicProcessor")]
		[MinLength(3)]
		[MaxLength(20)]
		[DataType(DataType.Text)]
		public string GraphicProcessor { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("Frequency")]
		[MinLength(3)]
		[MaxLength(5)]
		public int Frequency { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("VRAM")]
		[MinLength(2)]
		[MaxLength(5)]
		public int VRAM { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("VRAMType")]
		[MinLength(4)]
		[MaxLength(7)]
		public string VRAMType { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("VRAMFrequency")]
		[MinLength(3)]
		[MaxLength(5)]
		public int VRAMFrequency { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("VRAMBusWidth")]
		[MinLength(2)]
		[MaxLength(4)]
		public int VRAMBusWidth { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("DirectX")]
		[MinLength(1)]
		[MaxLength(3)]
		public int DirectX { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("SLI_Crossfire")]
		public bool SLIOrCrossfire { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("AdditionalPower")]
		[MinLength(1)]
		[MaxLength(2)]
		public int AdditionalPower { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("Width")]
		[MinLength(3)]
		[MaxLength(5)]
		public int Width { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("VGA")]
		[MinLength(1)]
		[MaxLength(1)]
		public int VGA { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("DVI")]
		[MinLength(1)]
		[MaxLength(1)]
		public int DVI { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("HDMI")]
		[MinLength(1)]
		[MaxLength(1)]
		public int HDMI { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("DisplayPort")]
		[MinLength(1)]
		[MaxLength(1)]
		public int DisplayPort { get; set; }
    }
}
