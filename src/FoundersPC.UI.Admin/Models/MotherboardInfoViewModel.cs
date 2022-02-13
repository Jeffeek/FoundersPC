namespace FoundersPC.UI.Admin.Models;

public class MotherboardInfoViewModel : HardwareInfoViewModel
{
    private int? _socketId;

    public int? SocketId
    {
        get => _socketId ?? -1;
        set => SetProperty(ref _socketId, value);
    }

    private int? _motherboardFactorId;
    public int? MotherboardFactorId
    {
        get => _motherboardFactorId ?? -1;
        set => SetProperty(ref _motherboardFactorId, value);
    }

    private int? _ramTypeId;
    public int? RAMTypeId
    {
        get => _ramTypeId ?? -1;
        set => SetProperty(ref _ramTypeId, value);
    }

    private int? _ramSlotsCount;
    public int? RAMSlotsCount
    {
        get => _ramSlotsCount;
        set => SetProperty(ref _ramSlotsCount, value);
    }

    private int? _ramModeId;
    public int? RAMModeId
    {
        get => _ramModeId ?? -1;
        set => SetProperty(ref _ramModeId, value);
    }

    private bool? _crossfireSupport;
    public bool? CrossfireSupport
    {
        get => _crossfireSupport;
        set => SetProperty(ref _crossfireSupport, value);
    }

    private bool? _sliSupport;
    public bool? SliSupport
    {
        get => _sliSupport;
        set => SetProperty(ref _sliSupport, value);
    }

    private string? _audioSupport;
    public string? AudioSupport
    {
        get => _audioSupport;
        set => SetProperty(ref _audioSupport, value);
    }

    private bool? _wiFiSupport;
    public bool? WiFiSupport
    {
        get => _wiFiSupport;
        set => SetProperty(ref _wiFiSupport, value);
    }

    private bool? _ps2Support;
    public bool? PS2Support
    {
        get => _ps2Support;
        set => SetProperty(ref _ps2Support, value);
    }

    private int? _m2SlotsCount;
    public int? M2SlotsCount
    {
        get => _m2SlotsCount;
        set => SetProperty(ref _m2SlotsCount, value);
    }

    private string? _pciExpressVersion;
    public string? PCIExpressVersion
    {
        get => _pciExpressVersion;
        set => SetProperty(ref _pciExpressVersion, value);
    }
}