namespace FoundersPC.API.Domain.Common.Interfaces.Hardware
{
    public interface IMotherboard
    {
        string Socket { get; set; }

        string Factor { get; set; }

        string RAMSupport { get; set; }

        int RAMSlots { get; set; }

        string RAMMode { get; set; }

        bool SLIOrCrossfire { get; set; }

        string AudioSupport { get; set; }

        bool WiFiSupport { get; set; }

        bool PS2Support { get; set; }

        int M2SlotsCount { get; set; }

        string PCIExpressVersion { get; set; }

        string Title { get; set; }
    }
}