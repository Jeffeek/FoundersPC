#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.Memory
{
    [Index(nameof(Id))]
    public class HDD : HardwareEntityBase, IEquatable<HDD>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Factor")]
        [Range(2.5, 3.5)]
        [Required]
        public double Factor { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Interface")]
        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string Interface { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Volume")]
        [Required]
        public int Volume { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("HeadSpeed")]
        public int HeadSpeed { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("BufferSize")]
        public int BufferSize { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Noise")]
        public int Noise { get; set; }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(HDD other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return HeadSpeed == other.HeadSpeed
                   && BufferSize == other.BufferSize
                   && Noise == other.Noise
                   && Volume == other.Volume
                   && Factor.Equals(other.Factor)
                   && Interface == other.Interface;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((HDD)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(HeadSpeed, BufferSize, Noise);

        #endregion
    }
}