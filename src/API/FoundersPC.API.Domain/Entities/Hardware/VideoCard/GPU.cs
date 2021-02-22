#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Common.Base;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.VideoCard
{
    [Index(nameof(Id))]
    public class GPU : HardwareEntityBase, IEquatable<GPU>, IGPU
    {
        [ForeignKey(nameof(GraphicsProcessorId))]
        public VideoCardCore Core { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("GraphicsProcessorId")]
        [MinLength(3)]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        [Required]
        public int GraphicsProcessorId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("AdditionalPower")]
        [Required]
        public int AdditionalPower { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Frequency")]
        [Required]
        public int Frequency { get; set; }

        [DataType(DataType.Text)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Series")]
        [Required]
        public string Series { get; set; }

        #region Memory

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VideoMemoryVolume")]
        [Required]
        public int VideoMemoryVolume { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VideoMemoryType")]
        [MinLength(4)]
        [MaxLength(8)]
        [Required]
        public string VideoMemoryType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VideoMemoryFrequency")]
        [Required]
        public int VideoMemoryFrequency { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VideoMemoryBusWidth")]
        [Required]
        public int VideoMemoryBusWidth { get; set; }

        #endregion

        #region Output

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VGA")]
        [Required]
        public int VGA { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("DVI")]
        [Required]
        public int DVI { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("HDMI")]
        [Required]
        public int HDMI { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("DisplayPort")]
        [Required]
        public int DisplayPort { get; set; }

        #endregion

        #region Equality members

        /// <inheritdoc />
        public bool Equals(GPU other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return GraphicsProcessorId == other.GraphicsProcessorId
                   && AdditionalPower == other.AdditionalPower
                   && VideoMemoryVolume == other.VideoMemoryVolume
                   && VideoMemoryType == other.VideoMemoryType
                   && VideoMemoryFrequency == other.VideoMemoryFrequency
                   && VideoMemoryBusWidth == other.VideoMemoryBusWidth
                   && VGA == other.VGA
                   && Frequency == other.Frequency
                   && Series == other.Series
                   && DVI == other.DVI
                   && HDMI == other.HDMI
                   && DisplayPort == other.DisplayPort;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((GPU)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(GraphicsProcessorId);
            hashCode.Add(Core);
            hashCode.Add(AdditionalPower);
            hashCode.Add(VideoMemoryVolume);
            hashCode.Add(VideoMemoryType);
            hashCode.Add(VideoMemoryFrequency);
            hashCode.Add(VideoMemoryBusWidth);
            hashCode.Add(VGA);
            hashCode.Add(DVI);
            hashCode.Add(HDMI);
            hashCode.Add(DisplayPort);

            return hashCode.ToHashCode();
        }

        #endregion
    }
}