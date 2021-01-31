using System;

namespace FoundersPC.Services.DTO
{
    public class ProcessorCoreUpdateDto
    {
	    public DateTime? MarketLaunch { get; set; }

	    public int Title { get; set; }

	    public string MicroArchitecture { get; set; }

	    public int TechProcess { get; set; }

	    public int L1Cache { get; set; }

	    public int L2Cache { get; set; }

	    public int MaxL3Cache { get; set; }

	    public string Socket { get; set; }
    }
}
