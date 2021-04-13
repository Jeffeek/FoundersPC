#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Users
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UsersEntrancesService> _logger;

        public UsersEntrancesService(IHttpClientFactory httpClientFactory,
                                     ILogger<UsersEntrancesService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client = _httpClientFactory.CreateClient("Get all users entrances client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{MicroservicesUrls.IdentityServer}Users/");

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>("Entrances");

            return responseMessage;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserEntranceLogReadDto>> GetPaginateableEntrancesAsync(int pageNumber,
                                                                                             int pageSize,
                                                                                             string adminToken)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            using var client = _httpClientFactory.CreateClient("Get users entrances client by paging");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{MicroservicesUrls.IdentityServer}Users/");

            var responseMessage =
                await client
                    .GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>
                    >($"Entrances?Page={pageNumber}&Size={pageSize}");

            return responseMessage;
        }

        public async Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client = _httpClientFactory.CreateClient($"Get user entrance with id = {id} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{MicroservicesUrls.IdentityServer}Users/Entrances/");

            var responseMessage = await client.GetFromJsonAsync<UserEntranceLogReadDto>($"{id}");

            return responseMessage;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesBetweenAsync(DateTime start,
                                                                                           DateTime finish,
                                                                                           string adminToken)
        {
            if (start > finish)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesBetweenAsync)}:{nameof(start)} was above than {nameof(finish)}");

                throw new ArgumentNullException(nameof(start));
            }

            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesBetweenAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client = _httpClientFactory.CreateClient("Get users entrances between");

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    $"{MicroservicesUrls.IdentityServer}Users/Entrances/");

            var responseMessage =
                await
                    client
                        .GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>
                        >($"Between?Start={start:s}&Finish={finish:s}");

            return responseMessage;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByIdAsync(int userId,
                                                                                            string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client = _httpClientFactory.CreateClient($"Get all user entrances with id = {userId} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{MicroservicesUrls.IdentityServer}Users/");

            var responseMessage =
                await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>($"ById/{userId}/Entrances");

            return responseMessage;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByEmailAsync(string userEmail,
                                                                                               string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client =
                _httpClientFactory.CreateClient($"Get all user entrances with email = {userEmail} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{MicroservicesUrls.IdentityServer}Users/");

            var responseMessage =
                await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>($"ByEmail/{userEmail}/Entrances");

            return responseMessage;
        }
    }
}