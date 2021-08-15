﻿#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public class Processor : HardwareBase, IEquatable<Processor>
    {
        public int? TDP { get; set; } = default!;

        public string? Series { get; set; } = default!;

        public int? MaxRamSpeed { get; set; } = default!;

        public int? CoresCount { get; set; } = default!;

        public int? ThreadsCount { get; set; } = default!;

        public int? Frequency { get; set; } = default!;

        public int? TurboBoostFrequency { get; set; } = default!;

        public int? TechProcess { get; set; } = default!;

        public int? L1Cache { get; set; } = default!;

        public int? L2Cache { get; set; } = default!;

        public int? L3Cache { get; set; } = default!;

        public int? IntegratedGraphicsId { get; set; } = default!;

        public VideoCard? IntegratedGraphics { get; set; }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(Processor? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return TDP == other.TDP
                   && Series == other.Series
                   && MaxRamSpeed == other.MaxRamSpeed
                   && CoresCount == other.CoresCount
                   && ThreadsCount == other.ThreadsCount
                   && Frequency == other.Frequency
                   && TurboBoostFrequency == other.TurboBoostFrequency
                   && TechProcess == other.TechProcess
                   && L1Cache == other.L1Cache
                   && L2Cache == other.L2Cache
                   && L3Cache == other.L3Cache
                   && IntegratedGraphicsId == other.IntegratedGraphicsId;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((Processor)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(TDP);
            hashCode.Add(Series);
            hashCode.Add(MaxRamSpeed);
            hashCode.Add(CoresCount);
            hashCode.Add(ThreadsCount);
            hashCode.Add(Frequency);
            hashCode.Add(TurboBoostFrequency);
            hashCode.Add(TechProcess);
            hashCode.Add(L1Cache);
            hashCode.Add(L2Cache);
            hashCode.Add(L3Cache);
            hashCode.Add(IntegratedGraphicsId);

            return hashCode.ToHashCode();
        }

        #endregion
    }
}