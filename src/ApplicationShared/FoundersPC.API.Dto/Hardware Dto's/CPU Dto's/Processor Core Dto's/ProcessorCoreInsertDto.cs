﻿#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Dto
{
    public class ProcessorCoreInsertDto
    {
        public DateTime? MarketLaunch { get; set; }

        public string Title { get; set; }

        public string MicroArchitecture { get; set; }

        public int L2CachePerCore { get; set; }

        public int L3CachePerCore { get; set; }

        public string Socket { get; set; }
    }
}