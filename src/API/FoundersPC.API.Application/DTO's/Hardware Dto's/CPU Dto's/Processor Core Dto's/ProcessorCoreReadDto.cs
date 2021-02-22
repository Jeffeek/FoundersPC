#region Using namespaces

using System;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;
using FoundersPC.ApplicationShared.Identity;

#endregion

namespace FoundersPC.API.Application
{
    public class ProcessorCoreReadDto : IProcessorCore, IIdentityItem
    {
        public int Id { get; set; }

        public DateTime? MarketLaunch { get; set; }

        public string Title { get; set; }

        public string MicroArchitecture { get; set; }

        public int L2Cache { get; set; }

        public int L3Cache { get; set; }

        public string Socket { get; set; }
    }
}