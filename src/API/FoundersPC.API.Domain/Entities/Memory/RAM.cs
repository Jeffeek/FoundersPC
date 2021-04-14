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
    public class RAM : HardwareEntityBase, IEquatable<RAM>
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
        [MinLength(5)]
        [MaxLength(8)]
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

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(RAM other)
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
                   && PCIndex == other.PCIndex;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((RAM)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(MemoryType,
                             Frequency,
                             CASLatency,
                             Timings,
                             Voltage,
                             XMP,
                             ECC,
                             PCIndex);

        #endregion
    }
}