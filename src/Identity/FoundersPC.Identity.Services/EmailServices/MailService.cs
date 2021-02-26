using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Smtp;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Domain.Settings;

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
                                                         Credentials = new NetworkCredential(_botConfiguration.MailAddress, _botConfiguration.Password)
                                                     });
        }

        public async Task<bool> SendToAsync(string email, string subject = "Unnamed", string content = "", bool html = false)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            var sendResult = await Email.From(_botConfiguration.MailAddress, "FoundersPC_daemon")
                                        .Subject(subject)
                                        .Body(content)
                                        .To(email)
                                        .SendAsync();

            return sendResult.Successful;
        }

        public async Task<bool> SendToManyAsync(IEnumerable<string> emails, string subject = "Unnamed", string content = "", bool html = false)
        {
            var sendResults = new List<bool>();
            foreach (var email in emails)
            {
                var send = await SendToAsync(email, subject, content);
                sendResults.Add(send);
            }

            return sendResults.All(x => x);
        }
    }
}
