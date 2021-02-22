#region Using namespaces

using FoundersPC.API.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.API.Application
{
    public class VideoCardCoreInsertDto : IVideoCardCore
    {
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