namespace FoundersPC.Domain.Common.Interfaces.Hardware
{
    public interface IVideoCardCore
    {
        int TechProcess { get; set; }
        string MaxResolution { get; set; }
        int MonitorsSupport { get; set; }
        string ConnectionInterface { get; set; }
        string Codename { get; set; }
        string DirectX { get; set; }
        bool SLIOrCrossfire { get; set; }
        string ArchitectureTitle { get; set; }
        string Title { get; set; }
    }
}
