#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Domain.Common.Interfaces.Hardware
{
    public interface IProcessorCore
    {
        DateTime? MarketLaunch { get; set; }

        string Title { get; set; }

        string MicroArchitecture { get; set; }

        int L2Cache { get; set; }

        int L3Cache { get; set; }

        string Socket { get; set; }
    }
}