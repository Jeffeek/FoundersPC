using System;
using System.Threading.Tasks;
using Amusoft.UI.WPF.Adorners;
using Amusoft.UI.WPF.Notifications;
using MediatR;

namespace FoundersPC.UI.Admin.Services;

internal static class NotificationHelper
{
    private const Position NotificationPosition = Position.BottomLeft;
    private const bool AutoClose = true;
    private static readonly TimeSpan AutoCloseTimeSpan = TimeSpan.FromSeconds(3);

    public static void ShowExceptionNotification(this NotificationHost notificationHost, Exception exception) =>
        notificationHost.DisplayAsync(BuildNotification($"Exception occured: {exception?.Message}", SimpleNotificationType.Error), NotificationPosition);

    public static void ShowInformationNotification(this NotificationHost notificationHost, string text) =>
        notificationHost.DisplayAsync(BuildNotification(text, SimpleNotificationType.Info), NotificationPosition);

    private static SimpleNotification BuildNotification(string text, SimpleNotificationType type) =>
        new(text, type)
        {
            AutoClose = AutoClose,
            AutoCloseDelay = AutoClose ? AutoCloseTimeSpan : TimeSpan.MaxValue
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