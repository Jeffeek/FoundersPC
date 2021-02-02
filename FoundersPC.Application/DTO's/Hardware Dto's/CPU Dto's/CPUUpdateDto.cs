using System;

namespace FoundersPC.Application
{
    public class CPUUpdateDto
    {
	    public int ProducerId { get; set; }

	    public DateTime? MarketLaunch { get; set; }

	    public int TDP { get; set; }

	    public ProcessorCoreReadDto Core { get; set; }

	    public string Name { get; set; }

	    public int MaxRamSpeed { get; set; }

	    public int TechProcess { get; set; }

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
