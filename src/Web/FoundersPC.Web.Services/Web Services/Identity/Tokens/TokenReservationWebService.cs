using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.RequestResponseShared.Response.Tokens;
using FoundersPC.Web.Application.Interfaces.Services.Pricing;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FoundersPC.Web.Services.Web_Services.Identity.Tokens
{
    // todo: inject
    public class TokenReservationWebService : ITokenReservationWebService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MicroservicesBaseAddresses _baseAddresses;

        public TokenReservationWebService(IHttpClientFactory httpClientFactory, MicroservicesBaseAddresses baseAddresses)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
        }

        public async Task<BuyNewTokenResponse> ReserveNewTokenAsync(TokenType type, string userEmail, string userJwtToken)
        {
            if (userJwtToken is null) throw new ArgumentNullException(nameof(userJwtToken));

            var client = _httpClientFactory.CreateClient("New api access token reservation client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme, userJwtToken, $"{_baseAddresses.IdentityApiBaseAddress}Tokens/");

            var requestModel = new BuyNewTokenRequest()
                               {
                                   TokenType = type,
                                   UserEmail = userEmail
                               };

            var responseMessage = await client.PostAsJsonAsync<BuyNewTokenRequest>("Reserve", requestModel);

            if (!responseMessage.IsSuccessStatusCode)
                throw new
                    WebException($"When tried to reserve token and send request to identity server, it returned a bad status code: {responseMessage.StatusCode}");

            var contentResult = await responseMessage.Content.ReadFromJsonAsync<BuyNewTokenResponse>();

            return contentResult;
        }
    }
}
