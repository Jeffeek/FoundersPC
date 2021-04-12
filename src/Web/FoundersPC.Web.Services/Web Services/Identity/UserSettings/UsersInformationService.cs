#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class UsersInformationService : IUsersInformationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersInformationService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<UserEntityReadDto> GetUserByIdAsync(int id, string adminToken)
        {
            if (id < 1)
                return null;

            using var client = _httpClientFactory.CreateClient("User by id client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{MicroservicesUrls.IdentityServer}Users/");

            var request = await client.GetFromJsonAsync<UserEntityReadDto>($"ById/{id}");

            return request;
        }

        public async Task<UserEntityReadDto> GetUserByEmailAsync(string email, string adminToken)
        {
            if (email is null)
                throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("User by email client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{MicroservicesUrls.IdentityServer}Users/");

            var request = await client.GetFromJsonAsync<UserEntityReadDto>($"ByEmail/{email}");

            return request;
        }

        public async Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync(string adminToken)
        {
            using var client = _httpClientFactory.CreateClient("UsersTable client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{MicroservicesUrls.IdentityServer}");

            var request = await client.GetFromJsonAsync<IEnumerable<UserEntityReadDto>>("Users");

            return request;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserEntityReadDto>> GetPaginateableUsersAsync(int pageNumber,
            int pageSize,
            string adminToken)
        {
            if (pageNumber <= 0
                || pageSize <= 0)
                return Enumerable.Empty<UserEntityReadDto>();

            if (adminToken is null)
                throw new ArgumentNullException(nameof(adminToken));

            using var client = _httpClientFactory.CreateClient("Get users");

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            var result =
                await client
                    .GetFromJsonAsync<IEnumerable<UserEntityReadDto>>($"Users?Page={pageNumber}&Size={pageSize}");

            return result;
        }
    }
}