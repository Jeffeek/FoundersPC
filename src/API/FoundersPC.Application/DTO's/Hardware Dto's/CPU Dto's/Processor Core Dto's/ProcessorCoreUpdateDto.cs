#region Using derectives

using System;
using FoundersPC.Domain.Common.Interfaces;
using FoundersPC.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.Application
{
	public class ProcessorCoreUpdateDto : IProcessorCore, IIdentityItem
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