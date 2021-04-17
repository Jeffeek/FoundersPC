#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.API.Domain.Entities.Processor;
using FoundersPC.API.Domain.Entities.VideoCard;
using FoundersPC.IdentityEntities.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Domain.Entities
{
    [Index(nameof(Id))]
    public class ProducerEntity : IdentityItem, IEquatable<ProducerEntity>
    {
        public ICollection<HardDriveDiskEntity> HardDrives { get; set; }

        public ICollection<SolidStateDriveEntity> SolidStateDrive { get; set; }

        public ICollection<RandomAccessMemoryEntity> RandomAccessMemory { get; set; }

        public ICollection<CaseEntity> Cases { get; set; }

        public ICollection<ProcessorEntity> Processors { get; set; }

        public ICollection<VideoCardEntity> VideoCards { get; set; }

        public ICollection<MotherboardEntity> Motherboards { get; set; }

        public ICollection<PowerSupplyEntity> PowerSupplies { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MinLength(2)]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        [Column("ShortName")]
        public string ShortName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MinLength(3)]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Column("FullName")]
        [Required]
        public string FullName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        [MinLength(3)]
        [DataType(DataType.Text)]
        [Column("Country")]
        public string Country { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(100)]
        [MinLength(5)]
        [DataType(DataType.Url)]
        [Column("Website")]
        public string Website { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Date)]
        [Column("FoundationDate")]
        public DateTime? FoundationDate { get; set; }

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(ProducerEntity other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return ShortName == other.ShortName
                   && FullName == other.FullName
                   && Country == other.Country
                   && Website == other.Website
                   && Nullable.Equals(FoundationDate, other.FoundationDate);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((ProducerEntity)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(ShortName,
                             FullName,
                             Country,
                             Website,
                             FoundationDate);

        #endregion
    }
}