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

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    public class UsersInformationWebService : IUsersInformationWebService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersInformationWebService(IHttpClientFactory httpClientFactory,
                                          MicroservicesBaseAddresses baseAddresses)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
        }

        public async Task<UserEntityReadDto> GetUserByIdAsync(int userId, string token)
        {
            if (userId < 1) return null;

            using var client = _httpClientFactory.CreateClient("User by id client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

            var request = await client.GetFromJsonAsync<UserEntityReadDto>($"Users/{userId}");

            return request;
        }

        public async Task<UserEntityReadDto> GetUserByEmailAsync(string email, string token)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("User by email client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

            var request = await client.GetFromJsonAsync<UserEntityReadDto>($"Users/{email}");

            return request;
        }

        public async Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync(string token)
        {
            using var client = _httpClientFactory.CreateClient("UsersTable client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

            var request = await client.GetFromJsonAsync<IEnumerable<UserEntityReadDto>>("Users");

            return request;
        }
    }
}