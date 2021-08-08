#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.Memory
{
    public class RandomAccessMemory : HardwareEntityBase, IEquatable<RandomAccessMemory>
    {
        public string? MemoryType { get; set; } = default!;

        public int? Frequency { get; set; } = default!;

        public string? CASLatency { get; set; } = default!;

        public string? Timings { get; set; } = default!;

        public double? Voltage { get; set; } = default!;

        public bool? XMP { get; set; } = default!;

        public bool? ECC { get; set; } = default!;

        public int? PCIndex { get; set; } = default!;

        public int? Volume { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(RandomAccessMemory other)
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

            return obj.GetType() == GetType() && Equals((RandomAccessMemory)obj);
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