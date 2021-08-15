#region Using namespaces

using System;
using FoundersPC.API.Domain.Entities.Metadatas;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    public class VideoCard : HardwareBase, IEquatable<VideoCard>
    {
        public int? TDP { get; set; }

        public int? AdditionalPower { get; set; } = default!;

        public int? Frequency { get; set; } = default!;

        public string? Series { get; set; } = default!;

        #region Memory

        public int? MemoryVolume { get; set; } = default!;

        public int? VideoMemoryId { get; set; } = default!;

        public int? MemoryFrequency { get; set; } = default!;

        public int? MemoryBusWidth { get; set; } = default!;

        public VideoMemory? VideoMemory { get; set; } = default!;

        #endregion

        #region Output

        public int? VGA { get; set; } = default!;

        public int? DVI { get; set; } = default!;

        public int? HDMI { get; set; } = default!;

        public int? DisplayPort { get; set; } = default!;

        #endregion

        #region Equality members

        /// <inheritdoc />
        public bool Equals(VideoCard? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return TDP == other.TDP
                   && AdditionalPower == other.AdditionalPower
                   && Frequency == other.Frequency
                   && Series == other.Series
                   && MemoryVolume == other.MemoryVolume
                   && VideoMemoryId == other.VideoMemoryId
                   && MemoryFrequency == other.MemoryFrequency
                   && MemoryBusWidth == other.MemoryBusWidth
                   && VGA == other.VGA
                   && DVI == other.DVI
                   && HDMI == other.HDMI
                   && DisplayPort == other.DisplayPort;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((VideoCard)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(TDP);
            hashCode.Add(AdditionalPower);
            hashCode.Add(Frequency);
            hashCode.Add(Series);
            hashCode.Add(MemoryVolume);
            hashCode.Add(VideoMemoryId);
            hashCode.Add(MemoryFrequency);
            hashCode.Add(MemoryBusWidth);
            hashCode.Add(VGA);
            hashCode.Add(DVI);
            hashCode.Add(HDMI);
            hashCode.Add(DisplayPort);

            return hashCode.ToHashCode();
        }

        #endregion
    }
}