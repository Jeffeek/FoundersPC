#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Common.Base;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
	[Index(nameof(Id))]
	public class Motherboard : HardwareEntityBase, IMotherboard, IEquatable<Motherboard>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Text)]
		[MaxLength(10)]
		[MinLength(3)]
		[Column("Socket")]
		[Required]
		public string Socket { get; set; }

		[MinLength(3)]
		[MaxLength(10)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Factor")]
		[Required]
		public string Factor { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("RAMSupport")]
		[MinLength(4)]
		[MaxLength(7)]
		[Required]
		public string RAMSupport { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("RAMSlots")]
		[Range(1, 6)]
		[Required]
		public int RAMSlots { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("RAMMode")]
		[MinLength(1)]
		[MaxLength(5)]
		[Required]
		public string RAMMode { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SLI_Crossfire")]
		[Required]
		public bool SLIOrCrossfire { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("AudioSupport")]
		[MinLength(3)]
		[MaxLength(20)]
		[Required]
		public string AudioSupport { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("WiFiSupport")]
		[Required]
		public bool WiFiSupport { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("PS2Support")]
		[Required]
		public bool PS2Support { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("M2SlotsCount")]
		[Range(0, 6)]
		[Required]
		public int M2SlotsCount { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("PCIExpressVersion")]
		[DataType(DataType.Text)]
		[MinLength(3)]
		[MaxLength(12)]
		[Required]
		public string PCIExpressVersion { get; set; }

		#region Equality members

		/// <inheritdoc />
		public bool Equals(Motherboard other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

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

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((Motherboard)obj);
		}

		/// <inheritdoc />
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