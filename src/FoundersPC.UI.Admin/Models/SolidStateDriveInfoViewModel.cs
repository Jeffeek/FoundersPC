namespace FoundersPC.UI.Admin.Models;

public class SolidStateDriveInfoViewModel : HardwareInfoViewModel
{
    private int? _diskFactorId;
    public int? DiskFactorId
    {
        get => _diskFactorId ?? -1;
        set => SetProperty(ref _diskFactorId, value);
    }

    private int? _diskConnectionInterfaceId;
    public int? DiskConnectionInterfaceId
    {
        get => _diskConnectionInterfaceId ?? -1;
        set => SetProperty(ref _diskConnectionInterfaceId, value);
    }

    private int? _volume;
    public int? Volume
    {
        get => _volume;
        set => SetProperty(ref _volume, value);
    }

    private string? _microScheme;
    public string? MicroScheme
    {
        get => _microScheme;
        set => SetProperty(ref _microScheme, value);
    }

    private double? _sequentialRead;
    public double? SequentialRead
    {
        get => _sequentialRead;
        set => SetProperty(ref _sequentialRead, value);
    }

    private double? _sequentialRecording;
    public double? SequentialRecording
    {
        get => _sequentialRecording;
        set => SetProperty(ref _sequentialRecording, value);
    }
}