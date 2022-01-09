using System;

namespace FoundersPC.UI.Admin.Locators;

public static class RefreshLocator
{
    public static event Action SaveRefresh;

    public static void FireSaveRefresh() => SaveRefresh?.Invoke();
}