#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Services.Users;
using FoundersPC.Application.UsersIdentity;
using Microsoft.Extensions.Configuration;

#endregion

namespace FoundersPC.Services.Users_Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration) => _configuration = configuration;

        #region Implementation of IEmailSenderService

        /// <inheritdoc />
        public Task<bool> SendConfirmationAsync(UserReadDto user) => throw new NotImplementedException();

        /// <inheritdoc />
        public Task<int> SendManyAsync(IEnumerable<UserReadDto> users, string content) => throw new NotImplementedException();

        #endregion
    }
}