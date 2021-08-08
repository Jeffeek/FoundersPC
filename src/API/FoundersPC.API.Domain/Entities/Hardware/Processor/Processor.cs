#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.Processor
{
    public class Processor : HardwareEntityBase, IEquatable<Processor>
    {
        public ProcessorCore Core { get; set; } = default!;

        public int? TDP { get; set; } = default!;

        public string? Series { get; set; } = default!;

        public int? ProcessorCoreId { get; set; } = default!;

        public int? MaxRamSpeed { get; set; } = default!;

        public int? CoresCount { get; set; } = default!;

        public int? Threads { get; set; } = default!;

        public int? Frequency { get; set; } = default!;

        public int? TurboBoostFrequency { get; set; } = default!;

        public int? TechProcess { get; set; } = default!;

        public int? L1Cache { get; set; } = default!;

        public int? L2Cache { get; set; } = default!;

        public int? L3Cache { get; set; } = default!;

        public bool? IntegratedGraphics { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(Processor other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return TDP == other.TDP
                   && Series == other.Series
                   && ProcessorCoreId == other.ProcessorCoreId
                   && Title == other.Title
                   && MaxRamSpeed == other.MaxRamSpeed
                   && CoresCount == other.CoresCount
                   && Threads == other.Threads
                   && Frequency == other.Frequency
                   && TurboBoostFrequency == other.TurboBoostFrequency
                   && TechProcess == other.TechProcess
                   && L1Cache == other.L1Cache
                   && L2Cache == other.L2Cache
                   && L3Cache == other.L3Cache
                   && IntegratedGraphics == other.IntegratedGraphics;
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

            return Equals((Processor)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(TDP);
            hashCode.Add(Series);
            hashCode.Add(ProcessorCoreId);
            hashCode.Add(Title);
            hashCode.Add(MaxRamSpeed);
            hashCode.Add(CoresCount);
            hashCode.Add(Threads);
            hashCode.Add(Frequency);
            hashCode.Add(TurboBoostFrequency);
            hashCode.Add(TechProcess);
            hashCode.Add(L1Cache);
            hashCode.Add(L2Cache);
            hashCode.Add(L3Cache);
            hashCode.Add(IntegratedGraphics);

            return hashCode.ToHashCode();
        }

        #endregion
    }
}