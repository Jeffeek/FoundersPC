using System;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Locators;

public class TitleBarLocator : BindableBase
{
    private bool _isLoading = true;

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            SetProperty(ref _isLoading, value);
            IsLoadingChanged?.Invoke(_isLoading);
        }
    }

    private int _currentFrameId;

    public int CurrentFrameId
    {
        get => _currentFrameId;
        set
        {
            SetProperty(ref _currentFrameId, value);
            TabChanged?.Invoke(_currentFrameId);
        }
    }

    public event Action<int>? TabChanged;

    public event Action<bool>? IsLoadingChanged;
}

internal static class TitleBarConstants
{
    public const int CasesPageId = 0;
    public const int CaseDetailsPageId = 1;
    public const int HardDriveDisksPageId = 2;
    public const int MotherboardsPageId = 3;
    public const int PowerSuppliesPageId = 4;
    public const int ProcessorsPageId = 5;
    public const int RandomAccessMemoriesPageId = 6;
    public const int SolidStateDrivesPageId = 7;
    public const int VideoCardsPageId = 8;
    public const int HardDriveDiskDetailsPageId = 9;
    public const int MotherboardDetailsPageId = 10;
    public const int PowerSupplyDetailsPageId = 11;
    public const int ProcessorDetailsPageId = 12;
    public const int RandomAccessMemoryDetailsPageId = 13;
    public const int SolidStateDriveDetailsPageId = 14;
    public const int VideoCardDetailsPageId = 15;
    public const int ProducersPageId = 16;
    public const int ProducerDetailsPageId = 17;
}