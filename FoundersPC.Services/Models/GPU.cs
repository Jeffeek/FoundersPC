using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
	public class GPU
    {
	    /// <inheritdoc />
	    public override string ToString() => $"{nameof(Id)}: {Id}," +
	                                         $" {nameof(CrystalSerialId)}: {CrystalSerialId}," +
	                                         $" {nameof(CrystalSerial)}: {CrystalSerial}," +
	                                         $" {nameof(Interface)}: {Interface}," +
	                                         $" {nameof(ProducerId)}: {ProducerId}," +
	                                         $" {nameof(Producer)}: {Producer}," +
	                                         $" {nameof(GraphicsProcessor)}: {GraphicsProcessor}," +
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

	    [Key]
	    [Column("Id")]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public int Id { get; set; }
	    
	    [Column("CrystalSerialId")]
	    [DatabaseGenerated((DatabaseGeneratedOption.None))]
	    public int CrystalSerialId { get; set; }
	    
	    [ForeignKey(nameof(CrystalSerialId))]
	    public CrystalSerial CrystalSerial { get; set; }
	    
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("Interface")]
		[MinLength(5)]
		[MaxLength(30)]
		[DataType(DataType.Text)]
		public string Interface { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("ProducerId")]
		public int ProducerId { get; set; }
		
		[ForeignKey(nameof(ProducerId))]
	    public Producer Producer { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("GraphicsProcessor")]
		[MinLength(3)]
		[MaxLength(20)]
		[DataType(DataType.Text)]
		public string GraphicsProcessor { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("Frequency")]
		[MinLength(3)]
		[MaxLength(5)]
		public int Frequency { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("VRAM")]
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
		public int AdditionalPower { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("Width")]
		public int Width { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("VGA")]
		public int VGA { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("DVI")]
		public int DVI { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("HDMI")]
		public int HDMI { get; set; }
		
		[DatabaseGenerated((DatabaseGeneratedOption.None))]
		[Column("DisplayPort")]
		public int DisplayPort { get; set; }
    }
}
