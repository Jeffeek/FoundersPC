#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Dto
{
    public class ProcessorCoreInsertDto
    {
        public DateTime? MarketLaunch { get; set; }

        public string Title { get; set; }

        public string MicroArchitecture { get; set; }

        public int L2Cache { get; set; }

        public int L3Cache { get; set; }

        public string Socket { get; set; }
    }
}