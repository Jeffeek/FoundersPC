namespace FoundersPC.UI.Admin.Models;

public class CaseInfoViewModel : HardwareInfoViewModel
{
    private int? _windowMaterialId;
    public int? WindowMaterialId
    {
        get => _windowMaterialId ?? -1;
        set
        {
            if (value <= 0)
                value = null;

            SetProperty(ref _windowMaterialId, value);
        }
    }

    private int? _caseTypeId;
    public int? CaseTypeId
    {
        get => _caseTypeId ?? -1;
        set
        {
            if (value <= 0)
                value = null;

            SetProperty(ref _caseTypeId, value);
        }
    }

    private double? _maxMotherboardSize;
    public double? MaxMotherboardSize
    {
        get => _maxMotherboardSize;
        set => SetProperty(ref _maxMotherboardSize, value);
    }

    private int? _materialId;
    public int? MaterialId
    {
        get => _materialId ?? -1;
        set
        {
            if (value <= 0)
                value = null;

            SetProperty(ref _materialId, value);
        }
    }

    private bool? _transparentWindow;
    public bool? TransparentWindow
    {
        get => _transparentWindow;
        set => SetProperty(ref _transparentWindow, value);
    }

    private int? _colorId;
    public int? ColorId
    {
        get => _colorId ?? -1;
        set
        {
            if (value <= 0)
                value = null;

            SetProperty(ref _colorId, value);
        }
    }

    private double? _weight;
    public double? Weight
    {
        get => _weight;
        set => SetProperty(ref _weight, value);
    }

    private double? _height;
    public double? Height
    {
        get => _height;
        set => SetProperty(ref _height, value);
    }

    private double? _width;
    public double? Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }

    private double? _depth;
    public double? Depth
    {
        get => _depth;
        set => SetProperty(ref _depth, value);
    }
}