namespace FoundersPC.API.Domain.Common.Interfaces.Hardware
{
    public interface IHDD
    {
        int HeadSpeed { get; set; }

        int BufferSize { get; set; }

        int Noise { get; set; }

        double Factor { get; set; }

        string Interface { get; set; }

        int Volume { get; set; }

        string Title { get; set; }
    }
}