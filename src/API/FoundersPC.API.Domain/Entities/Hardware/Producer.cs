#region Using namespaces

using System;
using System.Collections.Generic;
using FoundersPC.API.Domain.Common;
using FoundersPC.API.Domain.Entities.Hardware.Memory;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public class Producer : IdentityItem, IEquatable<Producer>
    {
        public ICollection<HardDriveDisk>? HardDrives { get; set; } = default!;

        public ICollection<SolidStateDrive>? SolidStateDrive { get; set; } = default!;

        public ICollection<RandomAccessMemory>? RandomAccessMemory { get; set; } = default!;

        public ICollection<Case>? Cases { get; set; } = default!;

        public ICollection<Processor.Processor>? Processors { get; set; } = default!;

        public ICollection<VideoCard.VideoCard>? VideoCards { get; set; } = default!;

        public ICollection<Motherboard>? Motherboards { get; set; } = default!;

        public ICollection<PowerSupply>? PowerSupplies { get; set; } = default!;

        public string? ShortName { get; set; } = default!;

        public string? FullName { get; set; } = default!;

        public string? Country { get; set; } = default!;

        public string? Website { get; set; } = default!;

        public DateTime? FoundationDate { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(Producer other)
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

            return obj.GetType() == GetType() && Equals((Producer)obj);
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