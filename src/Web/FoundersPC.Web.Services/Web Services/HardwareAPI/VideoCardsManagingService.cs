#region Using namespaces

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Response;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
    public class VideoCardsManagingService : BoilerplaitManagingService<VideoCardInsertDto, VideoCardReadDto, VideoCardUpdateDto>, IVideoCardsManagingService
    {
        public VideoCardsManagingService(IHttpClientFactory clientFactory,
                                         ILogger<IVideoCardsManagingService> logger) : base(clientFactory,
                                                                                            logger,
                                                                                            HardwareApiRoutes.VideoCardsEndpoint) { }

        /// <inheritdoc/>
        public VideoCardsManagingService(IHttpClientFactory clientFactory, ILogger logger, string apiRoute) : base(clientFactory, logger, apiRoute) { }

        #region Implementation of IVideoCardsManagingService

        #region Docs

        /// <inheritdoc/>
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
        /// <exception cref="T:System.ArgumentNullException">managerToken is <see langword="null"/></exception>

        #endregion

        public Task<IEnumerable<VideoCardReadDto>> GetAllVideoCardsAsync(string managerToken) => GetAllAsync(managerToken);

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentOutOfRangeException">id &lt; 1.</exception>
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

        public Task<VideoCardReadDto> GetVideoCardByIdAsync(int id, string managerToken) => GetByIdAsync(id, managerToken);

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">
        ///     A time-out occurred. For more information
        ///     about time-outs, see the Remarks section.
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
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Id &lt; 1.</exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public Task<bool> UpdateVideoCardAsync(int id, VideoCardUpdateDto videoCard, string managerToken) => UpdateAsync(id, videoCard, managerToken);

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.Net.Http.HttpRequestException">
        ///     The request failed due to an underlying issue such as network
        ///     connectivity, DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">
        ///     .NET Core and .NET 5.0 and later only: The request
        ///     failed due to timeout.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The request message was already sent by the <see cref="T:System.Net.Http.HttpClient"/> instance.
        ///     -or-
        ///     The requestUri is not an absolute URI.
        ///     -or-
        ///     <see cref="P:System.Net.Http.HttpClient.BaseAddress"/> is not set.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Manager token is null.</exception>
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

        public Task<bool> DeleteVideoCardAsync(int videoCardId, string managerToken) => DeleteAsync(videoCardId, managerToken);

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">producer is <see langword="null"/></exception>
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

        public Task<bool> CreateVideoCardAsync(VideoCardInsertDto videoCard, string managerToken) => CreateAsync(videoCard, managerToken);

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">managerToken is <see langword="null"/></exception>
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

        public Task<IPaginationResponse<VideoCardReadDto>> GetPaginateableVideoCardsAsync(int pageNumber, int pageSize, string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}