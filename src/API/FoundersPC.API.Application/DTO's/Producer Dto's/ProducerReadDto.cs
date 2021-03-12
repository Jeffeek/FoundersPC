#region Using namespaces

using System;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;
using FoundersPC.RepositoryShared.Identity;

#endregion

namespace FoundersPC.API.Application
{
	public class ProducerReadDto : IProducer, IIdentityItem
	{
		public int Id { get; set; }

		public string ShortName { get; set; }

		public string FullName { get; set; }

		public string Country { get; set; }

		public string Website { get; set; }

		public DateTime? FoundationDate { get; set; }
	}
}