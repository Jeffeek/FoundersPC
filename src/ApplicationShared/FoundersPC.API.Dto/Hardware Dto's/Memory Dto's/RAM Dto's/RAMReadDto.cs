#region Using namespaces

using FoundersPC.API.Dto.Base.Interfaces;

#endregion

namespace FoundersPC.API.Dto
{
    public class RAMReadDto : IProducerableDto
    {
        public int Id { get; set; }

        public string MemoryType { get; set; }

        public int Frequency { get; set; }

        public string CASLatency { get; set; }

        public string Timings { get; set; }

        public double Voltage { get; set; }

        public bool XMP { get; set; }

        public bool ECC { get; set; }

        public int PCIndex { get; set; }

        public string Title { get; set; }

        public int ProducerId { get; set; }

        public ProducerReadDto Producer { get; set; }
    }
}