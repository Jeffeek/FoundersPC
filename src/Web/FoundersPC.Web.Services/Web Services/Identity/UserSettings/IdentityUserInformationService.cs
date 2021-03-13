#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class IdentityUserInformationService : IIdentityUserInformationService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;

        public IdentityUserInformationService(IHttpClientFactory httpClientFactory,
                                              MicroservicesBaseAddresses microservicesBaseAddresses
        )
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = microservicesBaseAddresses;
        }

        public async Task<string> GetUserLoginAsync(string email, string token)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));
            if (token is null) throw new ArgumentNullException(nameof(token));

            using var client = _httpClientFactory.CreateClient("Login client");

            PrepareRequest(client, token);

            var loginResponse = await client.GetStringAsync($"user/settings/login/{email}");

            return loginResponse.Trim('"');
        }

        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokensAsync(string email, string token)
        {
            if (token is null) throw new ArgumentNullException(nameof(token));
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("User's tokens client");

            PrepareRequest(client, token);

            var tokensResponse = await client.GetFromJsonAsync<IEnumerable<ApiAccessUserTokenReadDto>>($"tokens/user/{email}");

            return tokensResponse;
        }

        public async Task<NotificationsSettingsViewModel> GetUserNotificationsAsync(string email, string token)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));
            if (token is null) throw new ArgumentNullException(nameof(token));

            using var client = _httpClientFactory.CreateClient("User's notifications settings client");

            PrepareRequest(client, token);

            var userNotificationsSettings = await client.GetFromJsonAsync<NotificationsSettingsViewModel>($"user/settings/notifications/{email}");

            return userNotificationsSettings;
        }

        private void PrepareRequest(HttpClient client, string token)
        {
            client.BaseAddress = new Uri(_baseAddresses.IdentityApiBaseAddress);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                                                                                       token);
        }
    }
}