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
    public const int HardDriveDiskDetailsPageId = 3;
    public const int MotherboardsPageId = 4;
    public const int MotherboardDetailsPageId = 5;
    public const int PowerSuppliesPageId = 6;
    public const int PowerSupplyDetailsPageId = 7;
    public const int ProcessorsPageId = 8;
    public const int ProcessorDetailsPageId = 9;
    public const int RandomAccessMemoriesPageId = 10;
    public const int RandomAccessMemoryDetailsPageId = 11;
    public const int SolidStateDrivesPageId = 12;
    public const int SolidStateDriveDetailsPageId = 13;
    public const int VideoCardsPageId = 14;
    public const int VideoCardDetailsPageId = 15;
    public const int ProducersPageId = 16;
    public const int ProducerDetailsPageId = 17;
    public const int UsersPageId = 18;
    public const int UserDetailsPageId = 19;
}

public class MainWindowTitleBarLocator : BindableBase
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

internal static class MainWindowTitleBarConstants
{
    public const int SignInPageId = 0;
    public const int AfterSignInPageId = 1;
}

internal enum RoleTypes
{
    System = 1,
    Administrator = 2,
    Manager = 3,
    DefaultUser = 4,
}