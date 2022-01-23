namespace FoundersPC.UI.Admin.Models;

public class PowerSupplyInfoViewModel : HardwareInfoViewModel
{
    private int? _power;
    public int? Power
    {
        get => _power;
        set => SetProperty(ref _power, value);
    }

    private int? _efficiency;
    public int? Efficiency
    {
        get => _efficiency;
        set => SetProperty(ref _efficiency, value);
    }

    private int? _motherboardPoweringId;
    public int? MotherboardPoweringId
    {
        get => _motherboardPoweringId;
        set => SetProperty(ref _motherboardPoweringId, value);
    }

    private bool? _isModular;
    public bool? IsModular
    {
        get => _isModular;
        set => SetProperty(ref _isModular, value);
    }

    private bool? _cpu4PIN;
    public bool? CPU4PIN
    {
        get => _cpu4PIN;
        set => SetProperty(ref _cpu4PIN, value);
    }

    private bool? _cpu8PIN;
    public bool? CPU8PIN
    {
        get => _cpu8PIN;
        set => SetProperty(ref _cpu8PIN, value);
    }

    private int? _fanDiameter;
    public int? FanDiameter
    {
        get => _fanDiameter;
        set => SetProperty(ref _fanDiameter, value);
    }

    private bool? _certificate80PLUS;
    public bool? Certificate80PLUS
    {
        get => _certificate80PLUS;
        set => SetProperty(ref _certificate80PLUS, value);
    }

    private bool? _pfc;
    public bool? PFC
    {
        get => _pfc;
        set => SetProperty(ref _pfc, value);
    }
}