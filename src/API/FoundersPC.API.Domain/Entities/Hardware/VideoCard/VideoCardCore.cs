#region Using namespaces

using System;
using System.Collections.Generic;
using FoundersPC.API.Domain.Common;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.VideoCard
{
    public class VideoCardCore : IdentityItem, IEquatable<VideoCardCore>
    {
        public ICollection<VideoCard> VideoCards { get; set; } = default!;

        public int? TechProcess { get; set; } = default!;

        public string? MaxResolution { get; set; } = default!;

        public int? MonitorsSupport { get; set; } = default!;

        public string? ConnectionInterface { get; set; } = default!;

        public string? Codename { get; set; } = default!;

        public string? DirectX { get; set; } = default!;

        public bool? SLIOrCrossfire { get; set; } = default!;

        public string? ArchitectureTitle { get; set; } = default!;

        public string? Title { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(VideoCardCore other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Title == other.Title
                   && TechProcess == other.TechProcess
                   && MaxResolution == other.MaxResolution
                   && MonitorsSupport == other.MonitorsSupport
                   && ConnectionInterface == other.ConnectionInterface
                   && DirectX == other.DirectX
                   && SLIOrCrossfire == other.SLIOrCrossfire
                   && ArchitectureTitle == other.ArchitectureTitle;
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

            return Equals((VideoCardCore)obj);
        }

        /// <inheritdoc/>
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