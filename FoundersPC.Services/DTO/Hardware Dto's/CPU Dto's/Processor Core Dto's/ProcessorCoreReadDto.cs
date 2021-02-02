﻿using System;

namespace FoundersPC.Services.DTO
{
    public class ProcessorCoreReadDto
    {
	    public int Id { get; set; }

		public DateTime? MarketLaunch { get; set; }

		public string Title { get; set; }

		public string MicroArchitecture { get; set; }

		public int L2CachePerCore { get; set; }

		public int L3CachePerCore { get; set; }

		public string Socket { get; set; }
	}
}
