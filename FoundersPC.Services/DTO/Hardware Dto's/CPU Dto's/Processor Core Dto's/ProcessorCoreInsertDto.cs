using System;

namespace FoundersPC.Services.DTO
{
    public class ProcessorCoreInsertDto
    {
	    public DateTime? MarketLaunch { get; set; }

	    public string Title { get; set; }

	    public string MicroArchitecture { get; set; }

	    public int TechProcess { get; set; }

	    public int L1Cache { get; set; }

	    public int L2Cache { get; set; }

	    public int MaxL3Cache { get; set; }

	    public string Socket { get; set; }
	}
}
