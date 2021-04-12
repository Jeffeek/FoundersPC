﻿#region Using namespaces

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.RequestResponseShared.Response.Tokens;
using FoundersPC.Web.Application.Interfaces.Services.Pricing;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Tokens
{
    public class TokenReservationWebService : ITokenReservationWebService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenReservationWebService(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        public async Task<BuyNewTokenResponse> ReserveNewTokenAsync(TokenType type,
                                                                    string userEmail,
                                                                    string userJwtToken)
        {
            if (userJwtToken is null)
                throw new ArgumentNullException(nameof(userJwtToken));

            using var client = _httpClientFactory.CreateClient("New api access token reservation client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        userJwtToken,
                                                        $"{MicroservicesUrls.IdentityServer}Tokens/");

            var requestModel = new BuyNewTokenRequest
                               {
                                   TokenType = type,
                                   UserEmail = userEmail
                               };

            var responseMessage = await client.PostAsJsonAsync("Reserve", requestModel);

            if (!responseMessage.IsSuccessStatusCode)
                throw new
                    WebException($"When tried to reserve token and send request to identity server, it returned a bad status code: {responseMessage.StatusCode}");

            var contentResult = await responseMessage.Content.ReadFromJsonAsync<BuyNewTokenResponse>();

            return contentResult;
        }
    }
}