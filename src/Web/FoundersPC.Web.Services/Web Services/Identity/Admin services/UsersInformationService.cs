using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    // todo: interface
    public class UsersInformationService : IUsersInformationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MicroservicesBaseAddresses _baseAddresses;

        public UsersInformationService(IHttpClientFactory httpClientFactory, MicroservicesBaseAddresses baseAddresses)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
        }

        public async Task<ApplicationUser> GetByIdAsync(int userId, string token)
        {
            if (userId < 1) return null;

            using var client = _httpClientFactory.CreateClient("User by id client");

            PrepareRequest(client, token);

            var request = await client.GetFromJsonAsync<ApplicationUser>($"Users/{userId}");

            return request;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email, string token)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("User by email client");

            PrepareRequest(client, token);

            var request = await client.GetFromJsonAsync<ApplicationUser>($"Users/{email}");

            return request;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll(string token)
        {
            using var client = _httpClientFactory.CreateClient("UsersTable client");

            PrepareRequest(client, token);

            var request = await client.GetFromJsonAsync<IEnumerable<ApplicationUser>>("Users");

            return request;
        }

        private void PrepareRequest(HttpClient client, string token)
        {
            client.BaseAddress = new Uri($"{_baseAddresses.IdentityApiBaseAddress}Admin/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                                                                                       token);
        }
    }
}
