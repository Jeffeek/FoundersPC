#region Using namespaces

using System;
using FoundersPC.API.Domain.Entities.Hardware.Additional;
using FoundersPC.API.Domain.Entities.Metadatas;
using FoundersPC.API.Domain.Enums;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public class Motherboard : HardwareBase, IEquatable<Motherboard>
    {
        public int? SocketId { get; set; } = default!;

        public int? MotherboardFactorId { get; set; } = default!;

        public int? RAMTypeId { get; set; } = default!;

        public int? RAMSlotsCount { get; set; } = default!;

        public int? RAMModeId { get; set; } = default!;

        public bool? CrossfireSupport { get; set; } = default!;

        public bool? SliSupport { get; set; }

        public string? AudioSupport { get; set; } = default!;

        public bool? WiFiSupport { get; set; } = default!;

        public bool? PS2Support { get; set; } = default!;

        public int? M2SlotsCount { get; set; } = default!;

        public string? PCIExpressVersion { get; set; } = default!;

        public Socket? Socket { get; set; } = default!;

        public MotherboardFactor? MotherboardFactor { get; set; } = default!;

        public RAMType? RAMType { get; set; } = default!;

        public RamMode? RAMModeType { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(Motherboard other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Socket == other.Socket
                   && MotherboardFactorId.Equals(other.MotherboardFactorId)
                   && RAMType == other.RAMType
                   && RAMSlotsCount == other.RAMSlotsCount
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
            hashCode.Add(MotherboardFactorId);
            hashCode.Add(RAMType);
            hashCode.Add(RAMSlotsCount);
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