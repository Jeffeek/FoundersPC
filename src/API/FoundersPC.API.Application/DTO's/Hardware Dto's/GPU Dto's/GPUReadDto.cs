#region Using namespaces

using FoundersPC.API.Application.Base.Interfaces;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;
using FoundersPC.ApplicationShared.Identity;

#endregion

namespace FoundersPC.API.Application
{
    public class GPUReadDto : IGPU, IIdentityItem, IProducerableDto
    {
        public int AdditionalPower { get; set; }

        public int Frequency { get; set; }

        public string Series { get; set; }

        public int VideoMemoryVolume { get; set; }

        public string VideoMemoryType { get; set; }

        public int VideoMemoryFrequency { get; set; }

        public int VideoMemoryBusWidth { get; set; }

        public int VGA { get; set; }

        public int DVI { get; set; }

        public int HDMI { get; set; }

        public int DisplayPort { get; set; }

        public string Title { get; set; }

        public int GraphicsProcessorId { get; set; }

        public int Id { get; set; }

        public int ProducerId { get; set; }

        public ProducerReadDto Producer { get; set; }
    }
}