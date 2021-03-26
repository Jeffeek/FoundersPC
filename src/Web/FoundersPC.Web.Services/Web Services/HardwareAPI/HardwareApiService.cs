#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
    public class HardwareApiService : IHardwareApiService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HardwareApiService> _logger;

        public HardwareApiService(IHttpClientFactory clientFactory,
                                  MicroservicesBaseAddresses baseAddresses,
                                  ILogger<HardwareApiService> logger)
        {
            _clientFactory = clientFactory;
            _baseAddresses = baseAddresses;
            _logger = logger;
        }

        public async Task<string> GetStringAsync(string entityType, string token)
        {
            if (entityType is null)
            {
                _logger.LogError($"{nameof(HardwareApiService)}: get string: entity type string was null");

                throw new ArgumentNullException(nameof(entityType));
            }

            if (token is null)
            {
                _logger.LogError($"{nameof(HardwareApiService)}: get string: token was null");

                throw new ArgumentNullException(nameof(token));
            }

            using var client = _clientFactory.CreateClient();

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme, token, _baseAddresses.IdentityApiBaseAddress);

            var request = await client.GetAsync(entityType);

            return await request.Content.ReadAsStringAsync();
        }
    }
}