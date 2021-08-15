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

        /// <inheritdoc/>
        public bool Equals(VideoCard other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return GraphicsProcessorId == other.GraphicsProcessorId
                   && AdditionalPower == other.AdditionalPower
                   && VideoMemoryVolume == other.VideoMemoryVolume
                   && VideoMemoryType == other.VideoMemoryType
                   && VideoMemoryFrequency == other.VideoMemoryFrequency
                   && VideoMemoryBusWidth == other.VideoMemoryBusWidth
                   && VGA == other.VGA
                   && Frequency == other.Frequency
                   && Series == other.Series
                   && DVI == other.DVI
                   && HDMI == other.HDMI
                   && DisplayPort == other.DisplayPort;
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

            return Equals((VideoCard)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(GraphicsProcessorId);
            hashCode.Add(Core);
            hashCode.Add(AdditionalPower);
            hashCode.Add(VideoMemoryVolume);
            hashCode.Add(VideoMemoryType);
            hashCode.Add(VideoMemoryFrequency);
            hashCode.Add(VideoMemoryBusWidth);
            hashCode.Add(VGA);
            hashCode.Add(DVI);
            hashCode.Add(HDMI);
            hashCode.Add(DisplayPort);

            return hashCode.ToHashCode();
        }

        #endregion
    }
}