#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Tokens
{
    public class UsersAccessTokensLogsService : IUsersAccessTokensLogsService
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsersAccessTokensLogsService(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsAsync(string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}Tokens/");

            return await client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>("Logs/All");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetPaginateableAccessTokensLogsAsync(int pageNumber,
                                                                                                   int pageSize,
                                                                                                   string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}Tokens/");

            return await client
                       .GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"Logs?Page={pageNumber}&Size={pageSize}");
        }

        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByUserIdAsync(int userId,
                                                                                               string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}Tokens/");

            return await client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"Logs/User/ById/{userId}");
        }

        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByUserEmailAsync(string userEmail,
                                                                                                  string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}Tokens/");

            return await client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"Logs/User/ByEmail/{userEmail}");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByTokenIdAsync(int tokenId, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}Tokens/");

            return await client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"Logs/Token/ById/{tokenId}");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByTokenAsync(string token, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));
            if (token is null) throw new ArgumentNullException(nameof(token));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}Tokens/");

            return await client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"Logs/Token/ByToken/{token}");
        }
    }
}