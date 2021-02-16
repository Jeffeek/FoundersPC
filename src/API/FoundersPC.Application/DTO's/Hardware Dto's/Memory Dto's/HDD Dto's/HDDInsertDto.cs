#region Using derectives

using FoundersPC.Application.Base.Interfaces;
using FoundersPC.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.Application
{
    public class HDDInsertDto : IHDD, IProducerIdentiable
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