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
using FoundersPC.RequestResponseShared.Pagination;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Tokens
{
    public class UsersAccessTokensLogsService : IUsersAccessTokensLogsService
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsersAccessTokensLogsService(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
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

        #endregion

        public Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsAsync(string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            return client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"{IdentityServerRoutes.Logs.TokenUsages.TokenUsagesEndpoint}/{ApplicationRestAddons.All}");
        }

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
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

        #endregion

        public async Task<IPaginationResponse<AccessTokenLogReadDto>> GetPaginateableAccessTokensLogsAsync(int pageNumber, int pageSize, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            return await client
                       .GetFromJsonAsync<PaginationResponse<AccessTokenLogReadDto>>($"{IdentityServerRoutes.Logs.TokenUsages.TokenUsagesEndpoint}{ApplicationRestAddons.BuildPageQuery(pageNumber, pageSize)}");
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
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
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByUserIdAsync(int userId,
                                                                                         string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            return client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"{IdentityServerRoutes.Logs.TokenUsages.TokenUsagesEndpoint}/{IdentityServerRoutes.BuildRouteById(IdentityServerRoutes.Tokens.Logs.LogsByUser.LogsByUserId, userId)}");
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
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
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByUserEmailAsync(string userEmail,
                                                                                            string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            return client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"{IdentityServerRoutes.Logs.TokenUsages.TokenUsagesEndpoint}/{IdentityServerRoutes.BuildRouteByEmail(IdentityServerRoutes.Logs.TokenUsages.ByUserEmail, userEmail)}");
        }

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken"/> is <see langword="null"/></exception>
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
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByTokenIdAsync(int tokenId, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            return client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"{IdentityServerRoutes.Tokens.Logs.LogsEndpoint}/{IdentityServerRoutes.BuildRouteById(IdentityServerRoutes.Tokens.Logs.LogsByToken.LogsByTokenId, tokenId)}");
        }

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>
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
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByTokenAsync(string token, string adminToken)
        {
            if (adminToken is null) throw new ArgumentNullException(nameof(adminToken));
            if (token is null) throw new ArgumentNullException(nameof(token));

            var client = _clientFactory.CreateClient();

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            return client.GetFromJsonAsync<IEnumerable<AccessTokenLogReadDto>>($"{IdentityServerRoutes.Tokens.Logs.LogsEndpoint}/{IdentityServerRoutes.BuildRouteForToken(IdentityServerRoutes.Tokens.Logs.LogsByToken.LogsByTokenString, token)}");
        }
    }
}