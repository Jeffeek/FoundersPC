#region Using namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.ApplicationShared.Identity;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.Processor
{
    public class ProcessorCore : IdentityItem, IEquatable<ProcessorCore>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MarketLaunch")]
        [DataType(DataType.Date)]
        public DateTime? MarketLaunch { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        [MinLength(3)]
        [Column("MicroArchitecture")]
        [Required]
        public string MicroArchitecture { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("L2CachePerCore")]
        public int L2CachePerCore { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("L3CachePerCore")]
        public int L3CachePerCore { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [MinLength(4)]
        [Column("Socket")]
        [Required]
        public string Socket { get; set; }

        [DataType(DataType.Text)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Title")]
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Title { get; set; }

        public ICollection<CPU> Processors { get; set; }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(ProcessorCore other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Nullable.Equals(MarketLaunch, other.MarketLaunch)
                   && Title == other.Title
                   && MicroArchitecture == other.MicroArchitecture
                   && L2CachePerCore == other.L2CachePerCore
                   && L3CachePerCore == other.L3CachePerCore
                   && Socket == other.Socket;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((ProcessorCore)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(MarketLaunch, Title, MicroArchitecture, L2CachePerCore, L3CachePerCore, Socket);

        #endregion
    }
}