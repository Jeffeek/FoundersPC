#region Using namespaces

using FoundersPC.API.Dto.Base.Interfaces;

#endregion

namespace FoundersPC.API.Dto
{
    public class HardDriveDiskReadDto : IProducerableDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int HeadSpeed { get; set; }

        public int BufferSize { get; set; }

        public int Noise { get; set; }

        public double Factor { get; set; }

        public string Interface { get; set; }

        public int Volume { get; set; }

        public ProducerReadDto Producer { get; set; }

        public int ProducerId { get; set; }
    }
}