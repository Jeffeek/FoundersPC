﻿using FoundersPC.Domain.Common.Interfaces;
using FoundersPC.Domain.Common.Interfaces.Hardware;

namespace FoundersPC.Application
{
	public class VideoCardCoreReadDto : IVideoCardCore, IIdentityItem
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
        public int Id { get; set; }
    }
}