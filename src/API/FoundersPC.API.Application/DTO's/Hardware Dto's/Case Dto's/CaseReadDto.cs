#region Using namespaces

using FoundersPC.API.Application.Base.Interfaces;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;
using FoundersPC.RepositoryShared.Identity;

#endregion

namespace FoundersPC.API.Application
{
	public class CaseReadDto : ICase, IIdentityItem, IProducerableDto
	{
		public string Type { get; set; }

		public string MaxMotherboardSize { get; set; }

		public string WindowMaterial { get; set; }

		public string Material { get; set; }

		public bool TransparentWindow { get; set; }

		public string Color { get; set; }

		public int? Depth { get; set; }

		public int? Height { get; set; }

		public string Title { get; set; }

		public double? Weight { get; set; }

		public int? Width { get; set; }

		public int Id { get; set; }

		public int ProducerId { get; set; }

		public ProducerReadDto Producer { get; set; }
	}
}