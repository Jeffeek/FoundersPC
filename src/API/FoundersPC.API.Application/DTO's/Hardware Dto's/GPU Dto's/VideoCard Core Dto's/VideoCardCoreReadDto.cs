#region Using namespaces

using FoundersPC.API.Domain.Common.Interfaces.Hardware;
using FoundersPC.RepositoryShared.Identity;

#endregion

namespace FoundersPC.API.Application
{
	public class VideoCardCoreReadDto : IVideoCardCore, IIdentityItem
	{
		public int Id { get; set; }

		public int TechProcess { get; set; }

		public string MaxResolution { get; set; }

		public int MonitorsSupport { get; set; }

		public string ConnectionInterface { get; set; }

		public string Codename { get; set; }

		public string DirectX { get; set; }

		public bool SLIOrCrossfire { get; set; }

		public string ArchitectureTitle { get; set; }

		public string Title { get; set; }
	}
}