#region Using namespaces

using FoundersPC.API.Application.Base.Interfaces;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;
using FoundersPC.RepositoryShared.Identity;

#endregion

namespace FoundersPC.API.Application
{
	public class SSDReadDto : ISSD, IIdentityItem, IProducerableDto
	{
		public int Id { get; set; }

		public int ProducerId { get; set; }

		public ProducerReadDto Producer { get; set; }

		public double Factor { get; set; }

		public string Interface { get; set; }

		public int Volume { get; set; }

		public string MicroScheme { get; set; }

		public int SequentialRead { get; set; }

		public int SequentialRecording { get; set; }

		public string Title { get; set; }
	}
}