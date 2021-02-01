using System;

namespace FoundersPC.Services.DTO
{
    public class CPUReadDto
    {
		public int Id { get; set; }

		public ProducerReadDto Producer { get; set; }

		public DateTime? MarketLaunch { get; set; }

		public int TDP { get; set; }

		public ProcessorCoreReadDto Core { get; set; }

		public string Name { get; set; }

		public int MaxRamSpeed { get; set; }

		public int Cores { get; set; }

		public int Threads { get; set; }

		public int Frequency { get; set; }

		public int TurboBoostFrequency { get; set; }

		public int L1Cache { get; set; }

		public int L2Cache { get; set; }

		public int L3Cache { get; set; }

		public bool IntegratedGraphics { get; set; }
	}
}
