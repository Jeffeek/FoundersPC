#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class IdentityUserInformationService : IIdentityUserInformationService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IdentityUserInformationService> _logger;

        public IdentityUserInformationService(IHttpClientFactory httpClientFactory,
                                              MicroservicesBaseAddresses microservicesBaseAddresses,
                                              ILogger<IdentityUserInformationService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = microservicesBaseAddresses;
            _logger = logger;
        }

        public async Task<ApplicationUser> GetOverallInformation(string email, string token)
        {
            if (email is null)
            {
                _logger.LogError($"{nameof(IdentityUserInformationService)}: get overall information: email was null");

                throw new ArgumentNullException(nameof(email));
            }

            if (token is null)
            {
                _logger.LogError($"{nameof(IdentityUserInformationService)}: get overall information: token was null");

                throw new ArgumentNullException(nameof(token));
            }

            using var client = _httpClientFactory.CreateClient("User's overall information settings client");

            PrepareRequest(client, token);

            var userInformation =
                await client.GetFromJsonAsync<ApplicationUser>($"User/Settings/OverallInformation/{email}");

            return userInformation;
        }

        private void PrepareRequest(HttpClient client, string token)
        {
            client.BaseAddress = new Uri(_baseAddresses.IdentityApiBaseAddress);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                                              token);
        }
    }
}