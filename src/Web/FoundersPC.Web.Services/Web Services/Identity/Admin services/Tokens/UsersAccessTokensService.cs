using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Tokens
{
    public class UsersAccessTokensService : IUsersAccessTokensService
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsersAccessTokensService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetAllUsersAccessTokensAsync(string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}Tokens/");

            return await client.GetFromJsonAsync<IEnumerable<ApiAccessUserTokenReadDto>>("All");
        }

        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetPaginateableTokensAsync(int pageNumber, int pageSize, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}");

            return await client.GetFromJsonAsync<IEnumerable<ApiAccessUserTokenReadDto>>($"Tokens?Page={pageNumber}&Size={pageSize}");
        }

        public async Task<bool> BlockTokenByIdAsync(int tokenId, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}");

            var requestResult = await client.PutAsync($"Tokens/Block/ById/{tokenId}", null!);

            return requestResult.IsSuccessStatusCode;
        }

        public async Task<bool> BlockTokenByStringTokenAsync(string token, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}");

            var requestResult = await client.PutAsync($"Tokens/Block/ByToken/{token}", null!);

            return requestResult.IsSuccessStatusCode;
        }
    }
}
