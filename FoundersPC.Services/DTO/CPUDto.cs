using System;

namespace FoundersPC.Services.DTO
{
    public class CPUDto
    {
	    public int Id { get; set; }

	    public int ProducerId { get; set; }

	    public ProducerDto Producer { get; set; }

	    public DateTime? MarketLaunch { get; set; }

		public int TechProcess { get; set; }

	    public string Lineup { get; set; }

	    public int TDP { get; set; }

	    public int MaxRamSpeed { get; set; }

	    public string Socket { get; set; }

	    public int Cores { get; set; }

	    public int Frequency { get; set; }

	    public int TurboBoostFrequency { get; set; }

	    public int L3Cache { get; set; }

	    public bool IntegratedGraphics { get; set; }
	}
}
