#region Using derectives

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Domain.Entities.Hardware.VideoCard
{
	public class VideoCardCore : IEquatable<VideoCardCore>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Id")]
		[Key]
		public int Id { get; set; }

		[MinLength(5)]
		[MaxLength(30)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Title")]
		public string Title { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TechProcess")]
		[Required]
		public int TechProcess { get; set; }

		[MinLength(5)]
		[MaxLength(20)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MaxResolution")]
		[Required]
		public string MaxResolution { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MonitorsSupport")]
		[Range(1,20)]
		[Required]
		public int MonitorsSupport { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Interface")]
		[MinLength(5)]
		[MaxLength(30)]
		[DataType(DataType.Text)]
		[Required]
		public string Interface { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Frequency")]
		[Range(1, 6)]
		[Required]
		public int Frequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("DirectX_Version")]
		[Range(0, 5)]
		public int DirectX { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SLI_Crossfire")]
		[Required]
		public bool SLIOrCrossfire { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Text)]
		[Column("ArchitectureTitle")]
		[Required]
		public string ArchitectureTitle { get; set; }

		public ICollection<GPU> VideoCards { get; set; }

		#region Equality members

		/// <inheritdoc />
		public bool Equals(VideoCardCore other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Title == other.Title &&
			       TechProcess == other.TechProcess &&
			       MaxResolution == other.MaxResolution &&
			       MonitorsSupport == other.MonitorsSupport &&
			       Interface == other.Interface &&
			       Frequency == other.Frequency &&
			       DirectX == other.DirectX &&
			       SLIOrCrossfire == other.SLIOrCrossfire &&
			       ArchitectureTitle == other.ArchitectureTitle;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((VideoCardCore)obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			var hashCode = new HashCode();
			hashCode.Add(Title);
			hashCode.Add(TechProcess);
			hashCode.Add(MaxResolution);
			hashCode.Add(MonitorsSupport);
			hashCode.Add(Interface);
			hashCode.Add(Frequency);
			hashCode.Add(DirectX);
			hashCode.Add(SLIOrCrossfire);
			hashCode.Add(ArchitectureTitle);
			return hashCode.ToHashCode();
		}

		#endregion
	}
}