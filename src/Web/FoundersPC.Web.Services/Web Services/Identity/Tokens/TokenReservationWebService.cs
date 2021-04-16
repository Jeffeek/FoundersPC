#region Using namespaces

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Tokens;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Tokens;
using FoundersPC.Web.Application.Interfaces.Services.Pricing;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Tokens
{
    public class TokenReservationWebService : ITokenReservationWebService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenReservationWebService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="userJwtToken"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.UriFormatException">Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException"/>, instead.
        ///     uriString is empty.
        ///     -or-
        ///     The scheme specified in uriString is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)"/>.
        ///     -or-
        ///     uriString contains too many slashes.
        ///     -or-
        ///     The password specified in uriString is not valid.
        ///     -or-
        ///     The host name specified in uriString is not valid.
        ///     -or-
        ///     The file name specified in uriString is not valid.
        ///     -or-
        ///     The user name specified in uriString is not valid.
        ///     -or-
        ///     The host or authority name specified in uriString cannot be terminated by backslashes.
        ///     -or-
        ///     The port number specified in uriString is not valid or cannot be parsed.
        ///     -or-
        ///     The length of uriString exceeds 65519 characters.
        ///     -or-
        ///     The length of the scheme specified in uriString exceeds 1023 characters.
        ///     -or-
        ///     There is an invalid character sequence in uriString.
        ///     -or-
        ///     The MS-DOS path specified in uriString must start with c:\\.</exception>
        /// <exception cref="T:System.Net.WebException">When tried to reserve token and send request to identity server, it returned a bad status code.</exception>

        #endregion

        public async Task<BuyNewTokenResponse> ReserveNewTokenAsync(TokenType type,
                                                                    string userEmail,
                                                                    string userJwtToken)
        {
            if (userJwtToken is null)
                throw new ArgumentNullException(nameof(userJwtToken));

            var client = _httpClientFactory.CreateClient("New api access token reservation client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        userJwtToken,
                                                        MicroservicesUrls.IdentityServer);

            var requestModel = new BuyNewTokenRequest
                               {
                                   TokenType = type,
                                   UserEmail = userEmail
                               };

            var responseMessage = await client.PostAsJsonAsync($"{IdentityServerRoutes.Tokens.Endpoint}/{IdentityServerRoutes.Tokens.ReserveNewToken}", requestModel);

            if (!responseMessage.IsSuccessStatusCode)
                throw new
                    WebException($"When tried to reserve token and send request to identity server, it returned a bad status code: {responseMessage.StatusCode}");

            var contentResult = await responseMessage.Content.ReadFromJsonAsync<BuyNewTokenResponse>();

            return contentResult;
        }
    }
}