#region Using derectives

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Domain.Common.Base;
using FoundersPC.Domain.Common.Interfaces.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Domain.Entities.Hardware.Processor
{
	[Index(nameof(Id))]
	public class CPU : HardwareEntityBase, IEquatable<CPU>, ICPU
	{
		[ForeignKey(nameof(ProcessorCoreId))]
		public ProcessorCore Core { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TDP")]
		[Range(3, 300)]
		[Required]
		public int TDP { get; set; }

		[MaxLength(15)]
		[MinLength(3)]
		[Column("Series")]
		[Required]
		public string Series { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ProcessorCoreId")]
		[Required]
		public int ProcessorCoreId { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MaxRamSpeed")]
		[Required]
		public int MaxRamSpeed { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Cores")]
		[Required]
		public int Cores { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Threads")]
		[Required]
		public int Threads { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Frequency")]
		[Required]
		public int Frequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TurboBoostFrequency")]
		[Required]
		public int TurboBoostFrequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TechProcess")]
		[Required]
		public int TechProcess { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("L1Cache")]
		[Required]
		public int L1Cache { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("L2Cache")]
		[Required]
		public int L2Cache { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("L3Cache")]
		[Required]
		public int L3Cache { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("IntegratedGraphics")]
		[Required]
		public bool IntegratedGraphics { get; set; }

		#region Equality members

		/// <inheritdoc />
		public bool Equals(CPU other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return TDP == other.TDP
				   && Series == other.Series
				   && ProcessorCoreId == other.ProcessorCoreId
				   && Title == other.Title
				   && MaxRamSpeed == other.MaxRamSpeed
				   && Cores == other.Cores
				   && Threads == other.Threads
				   && Frequency == other.Frequency
				   && TurboBoostFrequency == other.TurboBoostFrequency
				   && TechProcess == other.TechProcess
				   && L1Cache == other.L1Cache
				   && L2Cache == other.L2Cache
				   && L3Cache == other.L3Cache
				   && IntegratedGraphics == other.IntegratedGraphics;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((CPU)obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			var hashCode = new HashCode();
			hashCode.Add(TDP);
			hashCode.Add(Series);
			hashCode.Add(ProcessorCoreId);
			hashCode.Add(Title);
			hashCode.Add(MaxRamSpeed);
			hashCode.Add(Cores);
			hashCode.Add(Threads);
			hashCode.Add(Frequency);
			hashCode.Add(TurboBoostFrequency);
			hashCode.Add(TechProcess);
			hashCode.Add(L1Cache);
			hashCode.Add(L2Cache);
			hashCode.Add(L3Cache);
			hashCode.Add(IntegratedGraphics);

			return hashCode.ToHashCode();
		}

		#endregion
	}
}