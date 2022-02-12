using FoundersPC.UI.Admin.Locators;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class MainWindowViewModel : BindableBase
{
    public MainWindowTitleBarLocator MainWindowTitleBarLocator { get; }

    public MainWindowViewModel(MainWindowTitleBarLocator mainWindowTitleBarLocator)
    {
        MainWindowTitleBarLocator = mainWindowTitleBarLocator;
        RefreshLocator.SuccessLogIn += ClearSubscribe;
    }

    private void ClearSubscribe()
    {
        RefreshLocator.SuccessLogIn -= ClearSubscribe;
    }

    private bool _isMessaging;
    public bool IsMessaging
    {
        get => _isMessaging;
        set => SetProperty(ref _isMessaging, value);
    }

    private string? _message;
    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }
}