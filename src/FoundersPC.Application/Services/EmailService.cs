#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Application.Services;

public class EmailService : IEmailService
{
    private readonly IDateTimeService _dateTimeService;
    private readonly IFluentEmail _fluentEmail;
    private readonly ILogger<IEmailService> _logger;

    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///     <paramref name="port"/> cannot be less than zero.
    /// </exception>
    public EmailService(IDateTimeService dateTimeService,
                        IFluentEmail fluentEmail,
                        ILogger<EmailService> logger)
    {
        _dateTimeService = dateTimeService;
        _fluentEmail = fluentEmail;
        _logger = logger;
    }

    /// <exception cref="T:System.ArgumentNullException"><paramref name="email"/> is <see langword="null"/></exception>
    public async Task<bool> SendToAsync(string email,
                                        string subject = "Unnamed",
                                        string content = "",
                                        bool html = false)
    {
        if (email is null)
        {
            _logger.LogError($"{nameof(EmailService)}: email was null when tried to send message");

            throw new ArgumentNullException(nameof(email));
        }

        var sendResult = await _fluentEmail
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

    /// <exception cref="T:System.ArgumentNullException">email is <see langword="null"/></exception>
    public Task SendToManyAsync(IEnumerable<string> emails,
                                string subject = "Unnamed",
                                string content = "",
                                bool html = false)
    {
        var sendResults = emails.Select(email => SendToAsync(email,
                                                             subject,
                                                             content,
                                                             html))
                                .ToArray();

        return Task.WhenAll(sendResults);
    }

    /// <exception cref="T:System.ArgumentNullException">email is <see langword="null"/></exception>
    public Task<bool> SendEntranceNotificationAsync(string email)
    {
        var content = $"You've been entered to FoundersPC API at {_dateTimeService.Now}";

        return SendToAsync(email, "Entrance", content);
    }

    /// <exception cref="T:System.ArgumentNullException">email is <see langword="null"/></exception>
    public Task<bool> SendRegistrationNotificationAsync(string email, string subject = null)
    {
        var content =
            $"Thanks for registration in our service!{(subject == null ? String.Empty : $"{Environment.NewLine}{subject}")}";

        return SendToAsync(email, "Registration Notification", content);
    }

    /// <exception cref="T:System.ArgumentNullException">email is <see langword="null"/></exception>
    public Task<bool> SendAPIAccessTokenAsync(string email, string token)
    {
        var content =
            $"This is your token for getting access to our API: {token}{Environment.NewLine}Don't lose it, we will not restore it";

        return SendToAsync(email, "API Access Token", content);
    }

    /// <exception cref="T:System.ArgumentNullException">email is <see langword="null"/></exception>
    public Task<bool> SendNewPasswordAsync(string email, string password)
    {
        var content = $"Congrats! You've changed your password to {password}. Use it to get access to our site!";

        return SendToAsync(email, "Password Change", content);
    }

    /// <exception cref="T:System.ArgumentNullException">email is <see langword="null"/></exception>
    public Task<bool> SendBlockNotificationAsync(string email, string reason = null)
    {
        var content =
            $"You've banned in out service.{(reason == null ? String.Empty : $"{Environment.NewLine}Reason: {reason}")}";

        return SendToAsync(email, "Your account was blocked", content);
    }

    /// <exception cref="T:System.ArgumentNullException">email is <see langword="null"/></exception>
    public Task<bool> SendUnBlockNotificationAsync(string email, string reason = null)
    {
        const string content = "You've UnBanned in our service.";

        return SendToAsync(email, "Your account was UNBLOCKED", content);
    }
}