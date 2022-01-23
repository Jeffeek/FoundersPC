namespace FoundersPC.UI.Admin.Models;

public class VideoCardInfoViewModel : HardwareInfoViewModel
{
    private int? _tdp;
    public int? TDP
    {
        get => _tdp;
        set => SetProperty(ref _tdp, value);
    }

    private int? _additionalPower;
    public int? AdditionalPower
    {
        get => _additionalPower;
        set => SetProperty(ref _additionalPower, value);
    }

    private int? _frequency;
    public int? Frequency
    {
        get => _frequency;
        set => SetProperty(ref _frequency, value);
    }

    private string? _series;
    public string? Series
    {
        get => _series;
        set => SetProperty(ref _series, value);
    }

    private int? _memoryVolume;
    public int? MemoryVolume
    {
        get => _memoryVolume;
        set => SetProperty(ref _memoryVolume, value);
    }

    private int? _videoMemoryTypeId;
    public int? VideoMemoryTypeId
    {
        get => _videoMemoryTypeId ?? -1;
        set => SetProperty(ref _videoMemoryTypeId, value);
    }

    private int? _memoryFrequency;
    public int? MemoryFrequency
    {
        get => _memoryFrequency;
        set => SetProperty(ref _memoryFrequency, value);
    }

    private int? _memoryBusWidth;
    public int? MemoryBusWidth
    {
        get => _memoryBusWidth;
        set => SetProperty(ref _memoryBusWidth, value);
    }

    private int? _vga;
    public int? VGA
    {
        get => _vga;
        set => SetProperty(ref _vga, value);
    }

    private int? _dvi;
    public int? DVI
    {
        get => _dvi;
        set => SetProperty(ref _dvi, value);
    }

    private int? _hdmi;
    public int? HDMI
    {
        get => _hdmi;
        set => SetProperty(ref _hdmi, value);
    }

    private int? _displayPort;
    public int? DisplayPort
    {
        get => _displayPort;
        set => SetProperty(ref _displayPort, value);
    }

    private bool? _isIntegrated;
    public bool? IsIntegrated
    {
        get => _isIntegrated;
        set => SetProperty(ref _isIntegrated, value);
    }
}