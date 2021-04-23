#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Tokens
{
    public class UsersAccessTokensService : IUsersAccessTokensService
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsersAccessTokensService(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
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
        ///     The MS-DOS path specified in uriString must start with c:\\.
        /// </exception>

        #endregion

        public Task<IEnumerable<AccessTokenReadDto>> GetAllUsersAccessTokensAsync(string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            return client.GetFromJsonAsync<IEnumerable<AccessTokenReadDto>>($"{IdentityServerRoutes.Tokens.TokensEndpoint}/{ApplicationRestAddons.All}");
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
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
        ///     The MS-DOS path specified in uriString must start with c:\\.
        /// </exception>

        #endregion

        public async Task<IPaginationResponse<AccessTokenReadDto>> GetPaginateableTokensAsync(int pageNumber, int pageSize, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            return
                await
                    client
                        .GetFromJsonAsync<PaginationResponse<AccessTokenReadDto>
                        >($"{IdentityServerRoutes.Tokens.TokensEndpoint}{ApplicationRestAddons.BuildPageQuery(pageNumber, pageSize)}");
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">
        ///     The request failed due to an underlying issue such as network
        ///     connectivity, DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
        ///     .NET Core and .NET 5.0 and later only: The request
        ///     failed due to timeout.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The <paramref name="requestUri"/> must be an absolute URI or
        ///     <see cref="P:System.Net.Http.HttpClient.BaseAddress"/> must be set.
        /// </exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
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
        ///     The MS-DOS path specified in uriString must start with c:\\.
        /// </exception>
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">
        ///     A time-out occurred. For more information
        ///     about time-outs, see the Remarks section.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public async Task<bool> BlockTokenByIdAsync(int tokenId, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            var requestResult =
                await
                    client.PutAsync($"{IdentityServerRoutes.Tokens.TokensEndpoint}/{ApplicationRestAddons.BuildRouteById(IdentityServerRoutes.Tokens.BlockByTokenId, tokenId)}",
                                    null!);

            return requestResult.IsSuccessStatusCode;
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
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
        ///     The MS-DOS path specified in uriString must start with c:\\.
        /// </exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">
        ///     The request failed due to an underlying issue such as network
        ///     connectivity, DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
        ///     .NET Core and .NET 5.0 and later only: The request
        ///     failed due to timeout.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The <paramref name="requestUri"/> must be an absolute URI or
        ///     <see cref="P:System.Net.Http.HttpClient.BaseAddress"/> must be set.
        /// </exception>
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">
        ///     A time-out occurred. For more information
        ///     about time-outs, see the Remarks section.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public async Task<bool> BlockTokenByStringTokenAsync(string token, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            var requestResult =
                await
                    client.PutAsync($"{IdentityServerRoutes.Tokens.TokensEndpoint}/{IdentityServerRoutes.BuildRouteForToken(IdentityServerRoutes.Tokens.BlockByTokenString, token)}",
                                    null!);

            return requestResult.IsSuccessStatusCode;
        }
    }
}