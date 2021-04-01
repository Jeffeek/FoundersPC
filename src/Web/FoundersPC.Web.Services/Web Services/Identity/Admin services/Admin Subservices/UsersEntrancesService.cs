#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Admin_Subservices
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MicroservicesBaseAddresses _baseAddresses;

        public UsersEntrancesService(IHttpClientFactory httpClientFactory, MicroservicesBaseAddresses baseAddresses)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken)
        {
            // todo: logger
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _httpClientFactory.CreateClient("Get all users entrances client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme, adminToken, $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>("");

            return responseMessage;
        }

        public async Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken)
        {
            // todo: logger
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _httpClientFactory.CreateClient($"Get user entrance with id = {id} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme, adminToken, $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

            var responseMessage = await client.GetFromJsonAsync<UserEntranceLogReadDto>($"{id}");

            return responseMessage;
        }

        public Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesBetweenAsync(DateTime start, DateTime finish, string adminToken) => throw new NotImplementedException();

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByIdAsync(int userId, string adminToken)
        {
            // todo: logger
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _httpClientFactory.CreateClient($"Get all user entrances with id = {userId} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme, adminToken, $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>($"User/ById/{userId}");

            return responseMessage;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByEmailAsync(string userEmail, string adminToken)
        {
            // todo: logger
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            using var client = _httpClientFactory.CreateClient($"Get all user entrances with email = {userEmail} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme, adminToken, $"{_baseAddresses.IdentityApiBaseAddress}UsersEntrances/");

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>($"User/ByEmail/{userEmail}");

            return responseMessage;
        }
    }
}