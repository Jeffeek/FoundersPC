using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;

namespace FoundersPC.Web.Services.Web_Services.Identity.Authentication
{
    public class IdentityUserInformationService : IIdentityUserInformationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MicroservicesBaseAddresses _baseAddresses;

        public IdentityUserInformationService(IHttpClientFactory httpClientFactory,
                                              MicroservicesBaseAddresses microservicesBaseAddresses)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = microservicesBaseAddresses;
        }

        private void PrepareRequest(HttpClient client)
        {
            client.BaseAddress = new Uri(_baseAddresses.IdentityApiBaseAddress);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }

        public async Task<string> GetUserLoginAsync(string email)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("Login client");

            PrepareRequest(client);

            var loginResponse = await client.GetStringAsync($"user/settings/login/{email}");

            return loginResponse.Trim('"');
        }

        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokensAsync(string email)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("User's tokens client");

            PrepareRequest(client);

            var tokensResponse = await client.GetFromJsonAsync<IEnumerable<ApiAccessUserTokenReadDto>>($"tokens/user/{email}");

            return tokensResponse;
        }

        public async Task<NotificationsSettingsViewModel> GetUserNotificationsAsync(string email)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("User's notifications settings client");

            PrepareRequest(client);

            var userNotificationsSettings = await client.GetFromJsonAsync<NotificationsSettingsViewModel>($"user/settings/notifications/{email}");

            return userNotificationsSettings;
        }
    }
}
