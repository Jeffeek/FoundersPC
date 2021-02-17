namespace FoundersPC.Domain.Common.Interfaces.Hardware
{
    public interface IRAM
    {
        string MemoryType { get; set; }

        int Frequency { get; set; }

        string CASLatency { get; set; }

        string Timings { get; set; }

        double Voltage { get; set; }

        bool XMP { get; set; }

        bool ECC { get; set; }

        int PCIndex { get; set; }

        string Title { get; set; }
    }
}