#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.Memory
{
    public class SolidStateDrive : HardwareEntityBase, IEquatable<SolidStateDrive>
    {
        public double? Factor { get; set; } = default!;

        public string? Interface { get; set; } = default!;

        public int? Volume { get; set; } = default!;

        public string? MicroScheme { get; set; } = default!;

        public int? SequentialRead { get; set; } = default!;

        public int? SequentialRecording { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(SolidStateDrive other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return MicroScheme == other.MicroScheme
                   && SequentialRead == other.SequentialRead
                   && SequentialRecording == other.SequentialRecording
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

            return Equals((SolidStateDrive)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(MicroScheme, SequentialRead, SequentialRecording);

        #endregion
    }
}