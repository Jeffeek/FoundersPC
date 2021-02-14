#region Using derectives

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Domain.Entities.Hardware.Memory
{
	public class SSD : DriveBase, IEquatable<SSD>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MicroScheme")]
		[MinLength(3)]
		[MaxLength(50)]
		[Required]
		public string MicroScheme { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SequentialRead")]
		[Required]
		public int SequentialRead { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SequentialRecording")]
		[Required]
		public int SequentialRecording { get; set; }

		#region Equality members

		/// <inheritdoc />
		public bool Equals(SSD other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return MicroScheme == other.MicroScheme &&
			       SequentialRead == other.SequentialRead &&
			       SequentialRecording == other.SequentialRecording &&
			       Volume == other.Volume &&
			       Factor.Equals(other.Factor) &&
			       Interface == other.Interface;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((SSD)obj);
		}

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Combine(MicroScheme, SequentialRead, SequentialRecording);

		#endregion
	}
}