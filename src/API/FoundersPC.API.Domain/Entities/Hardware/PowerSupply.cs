#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public class PowerSupply : HardwareEntityBase, IEquatable<PowerSupply>
    {
        public int? Power { get; set; } = default!;

        public int? Efficiency { get; set; } = default!;

        public string MotherboardPowering { get; set; } = default!;

        public bool? IsModular { get; set; } = default!;

        public bool? CPU4PIN { get; set; } = default!;

        public bool? CPU8PIN { get; set; } = default!;

        public int? FanDiameter { get; set; } = default!;

        public bool? Certificate80PLUS { get; set; } = default!;

        public bool? PFC { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(PowerSupply other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Power == other.Power
                   && Efficiency == other.Efficiency
                   && MotherboardPowering == other.MotherboardPowering
                   && IsModular == other.IsModular
                   && CPU4PIN == other.CPU4PIN
                   && CPU8PIN == other.CPU8PIN
                   && FanDiameter == other.FanDiameter
                   && Certificate80PLUS == other.Certificate80PLUS
                   && PFC == other.PFC;
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

            return Equals((PowerSupply)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Power);
            hashCode.Add(Efficiency);
            hashCode.Add(MotherboardPowering);
            hashCode.Add(IsModular);
            hashCode.Add(CPU4PIN);
            hashCode.Add(CPU8PIN);
            hashCode.Add(FanDiameter);
            hashCode.Add(Certificate80PLUS);
            hashCode.Add(PFC);

            return hashCode.ToHashCode();
        }

        #endregion
    }
}