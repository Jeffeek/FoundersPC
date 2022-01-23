namespace FoundersPC.UI.Admin.Models;

public class RandomAccessMemoryInfoViewModel : HardwareInfoViewModel
{
    private int? _ramTypeId;
    public int? RAMTypeId
    {
        get => _ramTypeId ?? -1;
        set => SetProperty(ref _ramTypeId, value);
    }

    private int? _frequency;
    public int? Frequency
    {
        get => _frequency;
        set => SetProperty(ref _frequency, value);
    }

    private string? _timings;
    public string? Timings
    {
        get => _timings;
        set => SetProperty(ref _timings, value);
    }

    private double? _voltage;
    public double? Voltage
    {
        get => _voltage;
        set => SetProperty(ref _voltage, value);
    }

    private bool? _xmp;
    public bool? XMP
    {
        get => _xmp;
        set => SetProperty(ref _xmp, value);
    }

    private bool? _ecc;
    public bool? ECC
    {
        get => _ecc;
        set => SetProperty(ref _ecc, value);
    }

    private int? _pcIndex;
    public int? PCIndex
    {
        get => _pcIndex;
        set => SetProperty(ref _pcIndex, value);
    }

    private int? _volume;
    public int? Volume
    {
        get => _volume;
        set => SetProperty(ref _volume, value);
    }
}