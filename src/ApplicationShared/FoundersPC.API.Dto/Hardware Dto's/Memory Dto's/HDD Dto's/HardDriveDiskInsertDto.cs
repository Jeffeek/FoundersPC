#region Using namespaces

using FoundersPC.API.Dto.Base.Interfaces;

#endregion

namespace FoundersPC.API.Dto
{
    public class HardDriveDiskInsertDto : IProducerIdentifiable
    {
        public int HeadSpeed { get; set; }

        public int BufferSize { get; set; }

        public int Noise { get; set; }

        public double Factor { get; set; }

        public string Interface { get; set; }

        public int Volume { get; set; }

        public string Title { get; set; }

        public int ProducerId { get; set; }
    }
}