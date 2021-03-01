﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Mail_service
{
    public interface IMailService
    {
        Task<bool> SendToAsync(string email, string subject = null, string content = null, bool html = false);

        Task<bool> SendToManyAsync(IEnumerable<string> emails, string subject = null, string content = null, bool html = false);

        Task<bool> SendEntranceNotificationAsync(string email);

        Task<bool> SendRegistrationNotificationAsync(string email);

        Task<bool> SendAPIAccessToken(string email, string token);

        Task<bool> SendNewPasswordAsync(string email, string password);
    }
}