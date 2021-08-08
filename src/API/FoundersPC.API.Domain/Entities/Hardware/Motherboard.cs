#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public class Motherboard : HardwareEntityBase, IEquatable<Motherboard>
    {
        public string? Socket { get; set; } = default!;

        public string? Factor { get; set; } = default!;

        public string? RAMSupport { get; set; } = default!;

        public int? RAMSlots { get; set; } = default!;

        public string? RAMMode { get; set; } = default!;

        public bool? SLIOrCrossfire { get; set; } = default!;

        public string? AudioSupport { get; set; } = default!;

        public bool? WiFiSupport { get; set; } = default!;

        public bool? PS2Support { get; set; } = default!;

        public int? M2SlotsCount { get; set; } = default!;

        public string? PCIExpressVersion { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(Motherboard other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Socket == other.Socket
                   && Factor.Equals(other.Factor)
                   && RAMSupport == other.RAMSupport
                   && RAMSlots == other.RAMSlots
                   && RAMMode == other.RAMMode
                   && SLIOrCrossfire == other.SLIOrCrossfire
                   && AudioSupport == other.AudioSupport
                   && WiFiSupport == other.WiFiSupport
                   && PS2Support == other.PS2Support
                   && M2SlotsCount == other.M2SlotsCount
                   && PCIExpressVersion == other.PCIExpressVersion;
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

            return Equals((Motherboard)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Socket);
            hashCode.Add(Factor);
            hashCode.Add(RAMSupport);
            hashCode.Add(RAMSlots);
            hashCode.Add(RAMMode);
            hashCode.Add(SLIOrCrossfire);
            hashCode.Add(AudioSupport);
            hashCode.Add(WiFiSupport);
            hashCode.Add(PS2Support);
            hashCode.Add(M2SlotsCount);
            hashCode.Add(PCIExpressVersion);

            return hashCode.ToHashCode();
        }

        #endregion
    }
}