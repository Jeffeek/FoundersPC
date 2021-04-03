﻿#region Using namespaces

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
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailBotConfiguration _botConfiguration;
        private readonly ILogger<IEmailService> _logger;

        public EmailService(EmailBotConfiguration botConfiguration, ILogger<EmailService> logger)
        {
            _botConfiguration = botConfiguration;
            _logger = logger;

            Email.DefaultSender = new SmtpSender(() =>
                                                     new SmtpClient(_botConfiguration.Host,
                                                                    _botConfiguration.Port)
                                                     {
                                                         EnableSsl = true,
                                                         DeliveryMethod = SmtpDeliveryMethod.Network,
                                                         DeliveryFormat = SmtpDeliveryFormat.International,
                                                         Credentials =
                                                             new NetworkCredential(_botConfiguration.MailAddress,
                                                                                   _botConfiguration.Password)
                                                     });
        }

        public async Task<bool> SendToAsync(string email,
                                            string subject = "Unnamed",
                                            string content = "",
                                            bool html = false)
        {
            if (email == null)
            {
                _logger.LogError($"{nameof(EmailService)}: email was null when tried to send message");

                throw new ArgumentNullException(nameof(email));
            }

            var sendResult = await Email.From(_botConfiguration.MailAddress, "FoundersPC_DAEMON")
                                        .Subject(subject)
                                        .Body(content, html)
                                        .To(email)
                                        .SendAsync();

            if (sendResult.Successful)
            {
                _logger.LogInformation($"Successful sending an email to: {email}");

                return true;
            }

            _logger.LogError($"Unsuccessful sending an email to: {email}");

            return false;
        }

        public async Task<bool> SendToManyAsync(IEnumerable<string> emails,
                                                string subject = "Unnamed",
                                                string content = "",
                                                bool html = false)
        {
            var sendResults = new List<bool>();

            foreach (var email in emails)
            {
                var sendResult = await SendToAsync(email,
                                                   subject,
                                                   content,
                                                   html);
                sendResults.Add(sendResult);
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
            var content =
                $"Thanks for registration in our service!{(subject == null ? String.Empty : $"{Environment.NewLine}{subject}")}";

            return await SendToAsync(email, "Registration Notification", content);
        }

        public Task<bool> SendAPIAccessTokenAsync(string email, string token)
        {
            var content =
                $"This is your token for getting access to our API: {token}{Environment.NewLine}Don't lose it, we will not restore it";

            return SendToAsync(email, "API Access Token", content);
        }

        public async Task<bool> SendNewPasswordAsync(string email, string password)
        {
            var content = $"Congrats! You've changed your password to {password}. Use it to get access to our site!";

            return await SendToAsync(email, "Password Change", content);
        }

        public async Task<bool> SendBlockNotificationAsync(string email, string reason = null)
        {
            var content =
                $"You've banned in out service.{(reason == null ? String.Empty : $"{Environment.NewLine}Reason: {reason}")}";

            return await SendToAsync(email, "Your account was blocked", content);
        }

        public async Task<bool> SendUnBlockNotificationAsync(string email, string reason = null)
        {
            var content = "You've UnBanned in out service.";

            return await SendToAsync(email, "Your account was UNBLOCKED", content);
        }
    }
}