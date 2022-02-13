using System;
using System.Threading.Tasks;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Notifications;
using MediatR;

namespace FoundersPC.UI.Admin.Services;

internal static class NotificationHelper
{
    private const Position DefaultNotificationPosition = Position.BottomLeft;
    private static readonly TimeSpan DefaultAutoCloseTimeSpan = TimeSpan.FromSeconds(3);

    public static void ShowExceptionNotification(this NotificationHost notificationHost, Exception exception) =>
        notificationHost.DisplayAsync(BuildNotification($"Exception occured: {exception?.Message}", SimpleNotificationType.Error, false), DefaultNotificationPosition);

    public static void ShowInformationNotification(this NotificationHost notificationHost, string text, bool autoClose = true) =>
        notificationHost.DisplayAsync(BuildNotification(text, SimpleNotificationType.Info, autoClose), DefaultNotificationPosition);

    public static void ShowDoneNotification(this NotificationHost notificationHost, string text, bool autoClose = true) =>
        notificationHost.DisplayAsync(BuildNotification(text, SimpleNotificationType.Done, autoClose), DefaultNotificationPosition);

    public static void ShowWarningNotification(this NotificationHost notificationHost, string text, bool autoClose = true) =>
        notificationHost.DisplayAsync(BuildNotification(text, SimpleNotificationType.Warning, autoClose), DefaultNotificationPosition);

    private static SimpleNotification BuildNotification(string text, SimpleNotificationType type, bool autoClose) =>
        new(text, type)
        {
            AutoClose = autoClose,
            AutoCloseDelay = autoClose ? DefaultAutoCloseTimeSpan : TimeSpan.MaxValue
        };

    public static async Task<TResult?> SendRequestWithNotification<TResult>(this NotificationHost notificationHost,
                                                                            ISender mediator,
                                                                            IRequest<TResult> request)
    {
        TResult? result = default;

        try
        {
            result = await mediator.Send(request);
        }
        catch (Exception e)
        {
            notificationHost.ShowExceptionNotification(e);
        }

        return result;
    }
}