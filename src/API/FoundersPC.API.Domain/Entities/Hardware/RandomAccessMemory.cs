#region Using namespaces

using System;
using FoundersPC.API.Domain.Entities.Metadatas;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public class RandomAccessMemory : HardwareBase, IEquatable<RandomAccessMemory>
    {
        public int? RAMTypeId { get; set; } = default!;

        public int? Frequency { get; set; } = default!;

        public string? Timings { get; set; } = default!;

        public double? Voltage { get; set; } = default!;

        public bool? XMP { get; set; } = default!;

        public bool? ECC { get; set; } = default!;

        public int? PCIndex { get; set; } = default!;

        public int? Volume { get; set; } = default!;

        public RAMType? RAMType { get; set; }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(RandomAccessMemory? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return RAMTypeId == other.RAMTypeId
                   && Frequency == other.Frequency
                   && Timings == other.Timings
                   && Nullable.Equals(Voltage, other.Voltage)
                   && XMP == other.XMP
                   && ECC == other.ECC
                   && PCIndex == other.PCIndex
                   && Volume == other.Volume;
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

            return Equals((RandomAccessMemory)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCode.Combine(RAMTypeId,
                             Frequency,
                             Timings,
                             Voltage,
                             XMP,
                             ECC,
                             PCIndex,
                             Volume);

        #endregion
    }
}