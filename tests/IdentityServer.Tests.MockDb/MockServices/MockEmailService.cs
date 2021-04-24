#region Using namespaces

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;

#endregion

namespace IdentityServer.Tests.MockDb.MockServices
{
    public class MockEmailService : IEmailService
    {
        #region Implementation of IEmailService

        /// <inheritdoc/>
        public Task<bool> SendToAsync(string email, string subject = null, string content = null, bool html = false)
        {
            Trace.WriteLine($"SendToAsync to {email}");

            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> SendToManyAsync(IEnumerable<string> emails, string subject = null, string content = null, bool html = false)
        {
            Trace.WriteLine($"SendToManyAsync to {String.Join(',', emails)}");

            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> SendEntranceNotificationAsync(string email)
        {
            Trace.WriteLine($"SendEntranceNotificationAsync to {email}");

            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> SendRegistrationNotificationAsync(string email, string subject = null)
        {
            Trace.WriteLine($"SendRegistrationNotificationAsync to {email}");

            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> SendAPIAccessTokenAsync(string email, string token)
        {
            Trace.WriteLine($"SendAPIAccessTokenAsync to {email}");

            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> SendNewPasswordAsync(string email, string password)
        {
            Trace.WriteLine($"SendNewPasswordAsync to {email}");

            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> SendBlockNotificationAsync(string email, string reason = null)
        {
            Trace.WriteLine($"SendBlockNotificationAsync to {email}");

            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public Task<bool> SendUnBlockNotificationAsync(string email, string reason = null)
        {
            Trace.WriteLine($"SendUnBlockNotificationAsync to {email}");

            return Task.FromResult(true);
        }

        #endregion
    }
}