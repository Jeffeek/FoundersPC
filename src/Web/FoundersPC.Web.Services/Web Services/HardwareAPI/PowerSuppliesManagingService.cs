#region Using namespaces

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
    public class PowerSuppliesManagingService : IPowerSuppliesManagingService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<PowerSuppliesManagingService> _logger;

        public PowerSuppliesManagingService(IHttpClientFactory clientFactory,
                                            ILogger<PowerSuppliesManagingService> logger)
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

        public async Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliesAsync(string managerToken)
        {
            if (managerToken is null)
            {
                _logger.LogError($"{nameof(PowerSuppliesManagingService)}:{nameof(GetAllPowerSuppliesAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient();

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<PowerSupplyReadDto>>($"{HardwareApiRoutes.PowerSuppliesEndpoint}");

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

        public async Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int id, string managerToken)
        {
            if (managerToken is null)
            {
                _logger.LogError($"{nameof(PowerSuppliesManagingService)}:{nameof(GetPowerSupplyByIdAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentOutOfRangeException(nameof(managerToken));
            }

            if (id < 1)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(GetPowerSupplyByIdAsync)}:{nameof(id)} was < 1");

                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var client = _clientFactory.CreateClient();

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.GetFromJsonAsync<PowerSupplyReadDto>($"{HardwareApiRoutes.PowerSuppliesEndpoint}/{id}");

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

        public async Task<bool> UpdatePowerSupplyAsync(int id, PowerSupplyUpdateDto powerSupply, string managerToken)
        {
            if (powerSupply is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(UpdatePowerSupplyAsync)}:{nameof(powerSupply)} was null");

                throw new ArgumentNullException(nameof(powerSupply));
            }

            if (id < 1)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(UpdatePowerSupplyAsync)}:{nameof(id)} was < 1");

                throw new ArgumentOutOfRangeException(nameof(id));
            }

            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(UpdatePowerSupplyAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient("Update producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage =
                await client.PutAsJsonAsync(ApplicationRestAddons.BuildRouteById($"{HardwareApiRoutes.PowerSuppliesEndpoint}/{ApplicationRestAddons.Update}", id),
                                            powerSupply);

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

        public async Task<bool> DeletePowerSupplyAsync(int powerSupplyId, string managerToken)
        {
            if (powerSupplyId < 1)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(DeletePowerSupplyAsync)}:{nameof(powerSupplyId)} was < 1");

                throw new ArgumentOutOfRangeException(nameof(powerSupplyId));
            }

            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(DeletePowerSupplyAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient();

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.DeleteAsync($"{HardwareApiRoutes.PowerSuppliesEndpoint}/{powerSupplyId}");

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

        public async Task<bool> CreatePowerSupplyAsync(PowerSupplyInsertDto producer, string managerToken)
        {
            if (producer is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(CreatePowerSupplyAsync)}:{nameof(producer)} was null");

                throw new ArgumentNullException(nameof(producer));
            }

            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(CreatePowerSupplyAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient("Create producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.PostAsJsonAsync(HardwareApiRoutes.PowerSuppliesEndpoint, producer);

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

        public async Task<IPaginationResponse<PowerSupplyReadDto>> GetPaginateablePowerSuppliesAsync(int pageNumber,
                                                                                                     int pageSize,
                                                                                                     string managerToken)
        {
            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(GetPaginateablePowerSuppliesAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentNullException(nameof(managerToken));
            }

            var client = _clientFactory.CreateClient();

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage =
                await client
                    .GetFromJsonAsync<PaginationResponse<PowerSupplyReadDto>
                    >($"{HardwareApiRoutes.PowerSuppliesEndpoint}{ApplicationRestAddons.BuildPageQuery(pageNumber, pageSize)}");

            return responseMessage;
        }
    }
}