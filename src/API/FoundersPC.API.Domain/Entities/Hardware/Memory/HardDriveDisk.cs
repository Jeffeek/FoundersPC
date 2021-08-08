#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.Memory
{
    public class HardDriveDisk : HardwareEntityBase, IEquatable<HardDriveDisk>
    {
        public double? Factor { get; set; } = default!;

        public string? Interface { get; set; } = default!;

        public int? Volume { get; set; } = default!;

        public int? HeadSpeed { get; set; } = default!;

        public int? BufferSize { get; set; } = default!;

        public int? Noise { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(HardDriveDisk other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return HeadSpeed == other.HeadSpeed
                   && BufferSize == other.BufferSize
                   && Noise == other.Noise
                   && Volume == other.Volume
                   && Factor.Equals(other.Factor)
                   && Interface == other.Interface;
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

            return Equals((HardDriveDisk)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(HeadSpeed, BufferSize, Noise);

        #endregion
    }
}