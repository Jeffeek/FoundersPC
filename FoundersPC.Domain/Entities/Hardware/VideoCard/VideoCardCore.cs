#region Using derectives

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace FoundersPC.Domain.Entities.Hardware.VideoCard
{
	public class VideoCardCore : IdentityItem, IEquatable<VideoCardCore>
	{
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
		[Range(1, 20)]
		[Required]
		public int MonitorsSupport { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ConnectionInterface")]
		[MinLength(4)]
		[MaxLength(30)]
		[DataType(DataType.Text)]
		[Required]
		public string ConnectionInterface { get; set; }

		[MinLength(5)]
		[MaxLength(30)]
		[Column("Codename")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Text)]
		public string Codename { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("DirectX_Version")]
		[Range(0, 13)]
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

		[DataType(DataType.Text)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Title")]
		[Required]
		public string Title { get; set; }

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
			       ConnectionInterface == other.ConnectionInterface &&
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
			hashCode.Add(ConnectionInterface);
			hashCode.Add(DirectX);
			hashCode.Add(SLIOrCrossfire);
			hashCode.Add(ArchitectureTitle);
			return hashCode.ToHashCode();
		}

		#endregion
	}
}