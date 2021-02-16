namespace FoundersPC.Domain.Common.Interfaces.Hardware
{
    public interface ICase
    {
        string Type { get; set; }
        string MaxMotherboardSize { get; set; }
        string Material { get; set; }
        bool TransparentWindow { get; set; }
        string Color { get; set; }
        int? Depth { get; set; }
        int? Height { get; set; }
        string Title { get; set; }
        double? Weight { get; set; }
        int? Width { get; set; }
    }
}
