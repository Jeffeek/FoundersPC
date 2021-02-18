#region Using namespaces

using FoundersPC.Application.Base.Interfaces;
using FoundersPC.Domain.Common.Interfaces;
using FoundersPC.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.Application
{
    public class HDDReadDto : IHDD, IIdentityItem, IProducerableDto
    {
        public string Title { get; set; }

        public int HeadSpeed { get; set; }

        public int BufferSize { get; set; }

        public int Noise { get; set; }

        public double Factor { get; set; }

        public string Interface { get; set; }

        public int Volume { get; set; }

        public int Id { get; set; }

        public ProducerReadDto Producer { get; set; }

        public int ProducerId { get; set; }
    }
}