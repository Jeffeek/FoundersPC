#region Using derectives

using FoundersPC.Application.Base.Interfaces;
using FoundersPC.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.Application
{
    public class RAMInsertDto : IRAM, IProducerIdentiable
    {
        public int ProducerId { get; set; }

        public string MemoryType { get; set; }

        public int Frequency { get; set; }

        public string CASLatency { get; set; }

        public string Timings { get; set; }

        public double Voltage { get; set; }

        public bool XMP { get; set; }

        public bool ECC { get; set; }

        public int PCIndex { get; set; }

        public string Title { get; set; }
    }
}