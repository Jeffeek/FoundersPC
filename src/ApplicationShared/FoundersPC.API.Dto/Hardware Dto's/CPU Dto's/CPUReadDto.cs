#region Using namespaces

using FoundersPC.API.Dto.Base.Interfaces;

#endregion

namespace FoundersPC.API.Dto
{
    public class CPUReadDto : IProducerableDto
    {
        public ProcessorCoreReadDto Core { get; set; }

        public int TDP { get; set; }

        public string Title { get; set; }

        public int MaxRamSpeed { get; set; }

        public int Cores { get; set; }

        public int Threads { get; set; }

        public int Frequency { get; set; }

        public int TurboBoostFrequency { get; set; }

        public int TechProcess { get; set; }

        public int L1Cache { get; set; }

        public int L2Cache { get; set; }

        public int L3Cache { get; set; }

        public bool IntegratedGraphics { get; set; }

        public string Series { get; set; }

        public int ProcessorCoreId { get; set; }

        public int Id { get; set; }

        public int ProducerId { get; set; }

        public ProducerReadDto Producer { get; set; }
    }
}