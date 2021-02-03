#region Using derectives

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Domain.Entities.Hardware
{
	[Index(nameof(Id))]
	public class PowerSupply : EquipmentEntityBase, IEquatable<PowerSupply>
	{
		[Column("Power")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Power { get; set; }

		[Column("Efficiency")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Efficiency { get; set; }

		[MinLength(1)]
		[MaxLength(10)]
		[Column("MotherboardPowering")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public string MotherboardPowering { get; set; }

		[Column("IsModular")]
		[Required]
		public bool IsModular { get; set; }

		[Column("CPU4PIN")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public bool CPU4PIN { get; set; }

		[Column("CPU8PIN")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public bool CPU8PIN { get; set; }

		[Column("FanDiameter")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int FanDiameter { get; set; }

		[Column("Certificate80PLUS")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public bool Certificate80PLUS { get; set; }

		[Column("PFC")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public bool PFC { get; set; }

		#region Equality members

		/// <inheritdoc />
		public bool Equals(PowerSupply other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Power == other.Power &&
			       Efficiency == other.Efficiency &&
			       MotherboardPowering == other.MotherboardPowering &&
			       IsModular == other.IsModular &&
			       CPU4PIN == other.CPU4PIN &&
			       CPU8PIN == other.CPU8PIN &&
			       FanDiameter == other.FanDiameter &&
			       Certificate80PLUS == other.Certificate80PLUS &&
			       PFC == other.PFC;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((PowerSupply)obj);
		}

		/// <inheritdoc />
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