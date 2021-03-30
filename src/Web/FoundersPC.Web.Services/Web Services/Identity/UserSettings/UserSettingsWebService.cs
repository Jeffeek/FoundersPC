#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class UserSettingsWebService : IUserSettingsWebService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserSettingsWebService> _logger;

        public UserSettingsWebService(IHttpClientFactory httpClientFactory,
                                      MicroservicesBaseAddresses microservicesBaseAddresses,
                                      ILogger<UserSettingsWebService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = microservicesBaseAddresses;
            _logger = logger;
        }

        public async Task<UserEntityReadDto> GetOverallInformation(string email, string token)
        {
            if (email is null)
            {
                _logger.LogError($"{nameof(UserSettingsWebService)}: get overall information: email was null");

                throw new ArgumentNullException(nameof(email));
            }

            if (token is null)
            {
                _logger.LogError($"{nameof(UserSettingsWebService)}: get overall information: token was null");

                throw new ArgumentNullException(nameof(token));
            }

            using var client = _httpClientFactory.CreateClient("User's overall information settings client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        _baseAddresses.IdentityApiBaseAddress);

            var userInformation =
                await client.GetFromJsonAsync<UserEntityReadDto>($"User/Settings/OverallInformation/{email}");

            return userInformation;
        }
    }
}