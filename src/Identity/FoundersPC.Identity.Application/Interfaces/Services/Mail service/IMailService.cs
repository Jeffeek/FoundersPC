#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Mail_service
{
    public interface IMailService
    {
        Task<bool> SendToAsync(string email,
                               string subject = null,
                               string content = null,
                               bool html = false);

        Task<bool> SendToManyAsync(IEnumerable<string> emails,
                                   string subject = null,
                                   string content = null,
                                   bool html = false);

        Task<bool> SendEntranceNotificationAsync(string email);

        Task<bool> SendRegistrationNotificationAsync(string email, string subject = null);

        Task<bool> SendAPIAccessTokenAsync(string email, string token);

        Task<bool> SendNewPasswordAsync(string email, string password);

        Task<bool> SendBlockNotificationAsync(string email, string reason = null);

        Task<bool> SendUnBlockNotificationAsync(string email, string reason = null);
    }
}