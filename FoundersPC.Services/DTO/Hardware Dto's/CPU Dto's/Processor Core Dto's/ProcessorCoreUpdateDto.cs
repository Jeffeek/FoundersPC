using System;

namespace FoundersPC.Services.DTO
{
    public class ProcessorCoreUpdateDto
    {
	    public DateTime? MarketLaunch { get; set; }

	    public int Title { get; set; }

	    public string MicroArchitecture { get; set; }

	    public int L2CachePerCore { get; set; }

		public int L3CachePerCore { get; set; }

	    public string Socket { get; set; }
    }
}
