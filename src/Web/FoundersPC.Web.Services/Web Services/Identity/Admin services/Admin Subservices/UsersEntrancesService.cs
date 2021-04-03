﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Admin_Subservices
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UsersEntrancesService> _logger;

        public UsersEntrancesService(IHttpClientFactory httpClientFactory,
                                     MicroservicesBaseAddresses baseAddresses,
                                     ILogger<UsersEntrancesService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
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
                                                        $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>("");

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
                                                        $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

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
                                                    $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

            var responseMessage =
                await
                    client
                        .GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>($"Between?start={start:yyyyMMdd}&finish={finish:yyyyMMdd}");

            return responseMessage;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByIdAsync(int userId, string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client = _httpClientFactory.CreateClient($"Get all user entrances with id = {userId} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>($"ById/{userId}");

            return responseMessage;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByEmailAsync(string userEmail, string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client = _httpClientFactory.CreateClient($"Get all user entrances with email = {userEmail} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>($"User/ByEmail/{userEmail}");

            return responseMessage;
        }
    }
}