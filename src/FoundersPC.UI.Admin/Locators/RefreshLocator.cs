using System;

namespace FoundersPC.UI.Admin.Locators;

public static class RefreshLocator
{
    public static event Action SaveRefresh;

    public static void FireSaveRefresh() => SaveRefresh?.Invoke();

    public static event Action<bool, string?> Messaging;

    public static void FireMessaging(bool state, string? message = null) => Messaging?.Invoke(state, message);

    public static event Action SuccessLogIn;

    public static void FireSuccessLogIn() => SuccessLogIn?.Invoke();
}