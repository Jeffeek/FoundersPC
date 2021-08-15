#region Using namespaces

using System;
using FoundersPC.API.Domain.Entities.Metadatas;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public class SolidStateDrive : HardwareBase, IEquatable<SolidStateDrive>
    {
        public int? DiskFactorId { get; set; } = default!;

        public int? DiskConnectionInterfaceId { get; set; } = default!;

        public int? Volume { get; set; } = default!;

        public string? MicroScheme { get; set; } = default!;

        public double? SequentialRead { get; set; } = default!;

        public double? SequentialRecording { get; set; } = default!;

        public DiskFactor? Factor { get; set; } = default!;

        public DiskConnectionInterface? Interface { get; set; } = default!;

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