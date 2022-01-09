using FoundersPC.UI.Admin.Locators;
using MvvmCross.ViewModels;

namespace FoundersPC.UI.Admin.ViewModels;

public class AfterSignInPageViewModel : MvxViewModel
{
    public TitleBarLocator TitleBarLocator { get; }

    public AfterSignInPageViewModel(TitleBarLocator titleBarLocator)
    {
        TitleBarLocator = titleBarLocator;
        titleBarLocator.IsLoadingChanged += value =>
                                            {
                                                IsLoading = value;
                                            };
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }
}