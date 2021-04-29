#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Domain.Entities.Memory
{
    [Index(nameof(Id))]
    public class RandomAccessMemoryEntity : HardwareEntityBase, IEquatable<RandomAccessMemoryEntity>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MemoryType")]
        [MinLength(5)]
        [MaxLength(15)]
        [Required]
        public string MemoryType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Frequency")]
        [Required]
        public int Frequency { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CASLatency")]
        [MinLength(3)]
        [MaxLength(5)]
        [Required]
        public string CASLatency { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Timings")]
        [MinLength(2)]
        [MaxLength(11)]
        [Required]
        public string Timings { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Voltage")]
        [Required]
        public double Voltage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("XMP")]
        [Required]
        public bool XMP { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ECC")]
        [Required]
        public bool ECC { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("PCIndex")]
        [Required]
        public int PCIndex { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Volume")]
        [Required]
        public int Volume { get; set; }

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(RandomAccessMemoryEntity other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return MemoryType == other.MemoryType
                   && Frequency == other.Frequency
                   && CASLatency == other.CASLatency
                   && Timings == other.Timings
                   && Voltage.Equals(other.Voltage)
                   && XMP == other.XMP
                   && ECC == other.ECC
                   && PCIndex == other.PCIndex
                   && Volume == other.Volume;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((RandomAccessMemoryEntity)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(MemoryType,
                             Frequency * Volume,
                             CASLatency,
                             Timings,
                             Voltage,
                             XMP,
                             ECC,
                             PCIndex);

        #endregion
    }
}