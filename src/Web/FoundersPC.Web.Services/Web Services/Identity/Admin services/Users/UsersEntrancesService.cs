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

        #region Docs

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken" /> is <see langword="null" /></exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException" />, instead.
        ///     uriString is empty.
        ///     -or-
        ///     The scheme specified in uriString is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)" />.
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

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            var client = _httpClientFactory.CreateClient("Get all users entrances client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var responseMessage =
                await
                    client
                        .GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>
                        >($"{IdentityServerRoutes.Logs.UsersEntrances.UsersEntrancesEndpoint}/{ApplicationRestAddons.All}");

            return responseMessage;
        }

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Condition.</exception>
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
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>

        #endregion

        public async Task<IPaginationResponse<UserEntranceLogReadDto>> GetPaginateableEntrancesAsync(int pageNumber,
                                                                                                     int pageSize,
                                                                                                     string adminToken)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            var client = _httpClientFactory.CreateClient("Get users entrances client by paging");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var responseMessage =
                await client
                    .GetFromJsonAsync<PaginationResponse<UserEntranceLogReadDto>
                    >($"{IdentityServerRoutes.Logs.UsersEntrances.UsersEntrancesEndpoint}{ApplicationRestAddons.BuildPageQuery(pageNumber, pageSize)}");

            return responseMessage;
        }

        #region Docs

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken" /> is <see langword="null" /></exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException" />, instead.
        ///     uriString is empty.
        ///     -or-
        ///     The scheme specified in uriString is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)" />.
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

        public async Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            var client = _httpClientFactory.CreateClient($"Get user entrance with id = {id} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var responseMessage =
                await
                    client.GetFromJsonAsync<UserEntranceLogReadDto>(ApplicationRestAddons
                                                                        .BuildRouteById($"{IdentityServerRoutes.Logs.UsersEntrances.UsersEntrancesEndpoint}/{IdentityServerRoutes.Logs.UsersEntrances.ByUserId}",
                                                                                        id));

            return responseMessage;
        }

        #region Docs

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentNullException"><paramref name="start" /> is <see langword="null" /></exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException" />, instead.
        ///     uriString is empty.
        ///     -or-
        ///     The scheme specified in uriString is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)" />.
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

            var client = _httpClientFactory.CreateClient("Get users entrances between");

            client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                    adminToken,
                                                    MicroservicesUrls.IdentityServer);

            var responseMessage =
                await
                    client
                        .GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>
                        >($"{IdentityServerRoutes.Logs.UsersEntrances.UsersEntrancesEndpoint}/{IdentityServerRoutes.Logs.UsersEntrances.Between}{IdentityServerRoutes.BuildRouteForBetween(start, finish)}");

            return responseMessage;
        }

        #region Docs

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentNullException"><paramref name="adminToken" /> is <see langword="null" /></exception>
        /// <exception cref="T:System.UriFormatException">
        ///     Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException" />, instead.
        ///     uriString is empty.
        ///     -or-
        ///     The scheme specified in uriString is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)" />.
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

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByIdAsync(int userId,
                                                                                            string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            var client = _httpClientFactory.CreateClient($"Get all user entrances with id = {userId} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var responseMessage =
                await
                    client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>(ApplicationRestAddons
                                                                                     .BuildRouteById($"{IdentityServerRoutes.Logs.UsersEntrances.UsersEntrancesEndpoint}/{IdentityServerRoutes.Logs.UsersEntrances.ByUserId}",
                                                                                                     userId));

            return responseMessage;
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
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">
        ///     A time-out occurred. For more information
        ///     about time-outs, see the Remarks section.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByEmailAsync(string userEmail,
                                                                                               string adminToken)
        {
            if (adminToken is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllEntrancesAsync)}:{nameof(adminToken)} was null");

                throw new ArgumentNullException(nameof(adminToken));
            }

            var client =
                _httpClientFactory.CreateClient($"Get all user entrances with email = {userEmail} client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var responseMessage =
                await
                    client.GetFromJsonAsync<IEnumerable<UserEntranceLogReadDto>>(IdentityServerRoutes
                                                                                     .BuildRouteByEmail($"{IdentityServerRoutes.Logs.UsersEntrances.UsersEntrancesEndpoint}/{IdentityServerRoutes.Logs.UsersEntrances.ByUserEmail}",
                                                                                                        userEmail));

            return responseMessage;
        }
    }
}