namespace FoundersPC.Domain.Common.Interfaces.Hardware
{
    public interface IGPU
    {
        int AdditionalPower { get; set; }
        int Frequency { get; set; }
        string Series { get; set; }
        int VideoMemoryVolume { get; set; }
        string VideoMemoryType { get; set; }
        int VideoMemoryFrequency { get; set; }
        int VideoMemoryBusWidth { get; set; }
        int VGA { get; set; }
        int DVI { get; set; }
        int HDMI { get; set; }
        int DisplayPort { get; set; }
        string Title { get; set; }
        int GraphicsProcessorId {get;set;}
    }
}
