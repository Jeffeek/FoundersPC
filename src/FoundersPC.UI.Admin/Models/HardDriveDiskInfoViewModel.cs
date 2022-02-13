namespace FoundersPC.UI.Admin.Models;

public class HardDriveDiskInfoViewModel : HardwareInfoViewModel
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

    private int? _headSpeed;
    public int? HeadSpeed
    {
        get => _headSpeed;
        set => SetProperty(ref _headSpeed, value);
    }

    private int? _bufferSize;
    public int? BufferSize
    {
        get => _bufferSize;
        set => SetProperty(ref _bufferSize, value);
    }

    private double? _noise;
    public double? Noise
    {
        get => _noise;
        set => SetProperty(ref _noise, value);
    }
}