namespace FoundersPC.UI.Admin.Models;

public class ProcessorInfoViewModel : HardwareInfoViewModel
{
    private int? _tdp;
    public int? TDP
    {
        get => _tdp;
        set => SetProperty(ref _tdp, value);
    }

    private string? _series;
    public string? Series
    {
        get => _series;
        set => SetProperty(ref _series, value);
    }

    private int? _maxRamSpeed;
    public int? MaxRamSpeed
    {
        get => _maxRamSpeed;
        set => SetProperty(ref _maxRamSpeed, value);
    }

    private int? _coresCount;
    public int? CoresCount
    {
        get => _coresCount;
        set => SetProperty(ref _coresCount, value);
    }

    private int? _threadsCount;
    public int? ThreadsCount
    {
        get => _threadsCount;
        set => SetProperty(ref _threadsCount, value);
    }

    private int? _frequency;
    public int? Frequency
    {
        get => _frequency;
        set => SetProperty(ref _frequency, value);
    }

    private int? _turboBoostFrequency;
    public int? TurboBoostFrequency
    {
        get => _turboBoostFrequency;
        set => SetProperty(ref _turboBoostFrequency, value);
    }

    private int? _techProcessId;
    public int? TechProcessId
    {
        get => _techProcessId;
        set => SetProperty(ref _techProcessId, value);
    }

    private int? _l1Cache;
    public int? L1Cache
    {
        get => _l1Cache;
        set => SetProperty(ref _l1Cache, value);
    }

    private int? _l2Cache;
    public int? L2Cache
    {
        get => _l2Cache;
        set => SetProperty(ref _l2Cache, value);
    }

    private int? _l3Cache;
    public int? L3Cache
    {
        get => _l3Cache;
        set => SetProperty(ref _l3Cache, value);
    }

    private int? _integratedGraphicsId;
    public int? IntegratedGraphicsId
    {
        get => _integratedGraphicsId;
        set => SetProperty(ref _integratedGraphicsId, value);
    }

    private int? _socketId;
    public int? SocketId
    {
        get => _socketId;
        set => SetProperty(ref _socketId, value);
    }
}