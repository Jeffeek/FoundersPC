using FoundersPC.UI.Admin.Locators;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.ViewModels;

public class MainWindowViewModel : BindableBase
{
    public MainWindowTitleBarLocator MainWindowTitleBarLocator { get; }

    public MainWindowViewModel(MainWindowTitleBarLocator mainWindowTitleBarLocator) =>
        MainWindowTitleBarLocator = mainWindowTitleBarLocator;
}