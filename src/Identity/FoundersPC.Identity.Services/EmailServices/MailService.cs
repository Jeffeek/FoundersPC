#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Smtp;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Domain.Settings;

#endregion

namespace FoundersPC.Identity.Services.EmailServices
{
    public class MailService : IMailService
    {
        private readonly EmailBotConfiguration _botConfiguration;

        public MailService(EmailBotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;

            Email.DefaultSender = new SmtpSender(() =>
                                                     new SmtpClient(_botConfiguration.Host,
                                                                    _botConfiguration.Port)
                                                     {
                                                         EnableSsl = true,
                                                         DeliveryMethod = SmtpDeliveryMethod.Network,
                                                         DeliveryFormat = SmtpDeliveryFormat.International,
                                                         Credentials = new NetworkCredential(_botConfiguration.MailAddress,
                                                                                             _botConfiguration.Password)
                                                     });
        }

        public async Task<bool> SendToAsync(string email, string subject = "Unnamed", string content = "", bool html = false)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            var sendResult = await Email.From(_botConfiguration.MailAddress, "FoundersPC_DAEMON")
                                        .Subject(subject)
                                        .Body(content, html)
                                        .To(email)
                                        .SendAsync();

            return sendResult.Successful;
        }

        public async Task<bool> SendToManyAsync(IEnumerable<string> emails, string subject = "Unnamed", string content = "", bool html = false)
        {
            var sendResults = new List<bool>();

            foreach (var email in emails)
            {
                var send = await SendToAsync(email, subject, content, html);
                sendResults.Add(send);
            }

            return sendResults.All(x => x);
        }

        public async Task<bool> SendEntranceNotificationAsync(string email)
        {
            var content = $"You've been entered to FoundersPC API at {DateTime.Now}";

            return await SendToAsync(email, "Entrance", content);
        }

        public async Task<bool> SendRegistrationNotificationAsync(string email, string subject = null)
        {
            var content = $"Thanks for registration in our service!{(subject == null ? String.Empty : $"{Environment.NewLine}{subject}")}";

            return await SendToAsync(email, "Registration Notification", content);
        }

        public Task<bool> SendAPIAccessTokenAsync(string email, string token)
        {
            var content = $"This is your token for getting access to our API: {token}{Environment.NewLine}Don't lose it, we will not(and can't) restore it";

            return SendToAsync(email, "API Access Token", content);
        }

        public async Task<bool> SendNewPasswordAsync(string email, string password)
        {
            var content = $"Congratz! You've changed your password to {password}. Use it to get access to our site!";

            return await SendToAsync(email, "Password Change", content);
        }

        public async Task<bool> SendBlockNotificationAsync(string email, string reason = null)
        {
            var content = $"You've banned in out service. Thanks!{(reason == null ? String.Empty : $"{Environment.NewLine}Reason: {reason}")}";

            return await SendToAsync(email, "Your account was blocked", content);
        }
    }
}