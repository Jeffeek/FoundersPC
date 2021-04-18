﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Response;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
    public class ProducersManagingService : IProducersManagingService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ProducersManagingService> _logger;

        public ProducersManagingService(IHttpClientFactory clientFactory,
                                        ILogger<ProducersManagingService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        #region Docs

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="managerToken"/> is <see langword="null"/></exception>

        #endregion

        public async Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync(string managerToken)
        {
            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(GetAllProducersAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient("Producers getter client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<ProducerReadDto>>($"{HardwareApiRoutes.Producers}");

            return responseMessage;
        }

        #region Docs

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

        public async Task<ProducerReadDto> GetProducerByIdAsync(int id, string managerToken)
        {
            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(GetProducerByIdAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentOutOfRangeException(nameof(managerToken));
            }

            if (id < 1)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(GetProducerByIdAsync)}:{nameof(id)} was < 1");

                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var client = _clientFactory.CreateClient("Producer getter client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.GetFromJsonAsync<ProducerReadDto>($"{HardwareApiRoutes.Producers}/{id}");

            return responseMessage;
        }

        #region Docs

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
        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">
        ///     A time-out occurred. For more information
        ///     about time-outs, see the Remarks section.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>

        #endregion

        public async Task<bool> UpdateProducerAsync(int id, ProducerUpdateDto producer, string managerToken)
        {
            if (producer is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(UpdateProducerAsync)}:{nameof(producer)} was null");

                throw new ArgumentNullException(nameof(producer));
            }

            if (id < 1)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(UpdateProducerAsync)}:{nameof(id)} was < 1");

                throw new ArgumentOutOfRangeException(nameof(id));
            }

            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(UpdateProducerAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient("Update producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage =
                await client.PutAsJsonAsync(ApplicationRestAddons.BuildRouteById($"{HardwareApiRoutes.Producers}/{ApplicationRestAddons.Update}", id),
                                            producer);

            return responseMessage.IsSuccessStatusCode;
        }

        #region Docs

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
        ///     The <paramref name="requestUri"/> is not an absolute URI.
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

        public async Task<bool> DeleteProducerAsync(int producerId, string managerToken)
        {
            if (producerId < 1)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(DeleteProducerAsync)}:{nameof(producerId)} was < 1");

                throw new ArgumentOutOfRangeException(nameof(producerId));
            }

            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(DeleteProducerAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient("Delete producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.DeleteAsync($"{HardwareApiRoutes.Producers}/{producerId}");

            return responseMessage.IsSuccessStatusCode;
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="producer"/> is <see langword="null"/></exception>
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

        public async Task<bool> CreateProducerAsync(ProducerInsertDto producer, string managerToken)
        {
            if (producer is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(CreateProducerAsync)}:{nameof(producer)} was null");

                throw new ArgumentNullException(nameof(producer));
            }

            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(CreateProducerAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient("Create producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.PostAsJsonAsync(HardwareApiRoutes.Producers, producer);

            return responseMessage.IsSuccessStatusCode;
        }

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="managerToken"/> is <see langword="null"/></exception>
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

        public async Task<IPaginationResponse<ProducerReadDto>> GetPaginateableProducersAsync(int pageNumber,
                                                                                              int pageSize,
                                                                                              string managerToken)
        {
            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(GetPaginateableProducersAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient("Producers getter client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage =
                await client
                    .GetFromJsonAsync<PaginationResponse<ProducerReadDto>
                    >($"{HardwareApiRoutes.Producers}{ApplicationRestAddons.BuildPageQuery(pageNumber, pageSize)}");

            return responseMessage;
        }
    }
}