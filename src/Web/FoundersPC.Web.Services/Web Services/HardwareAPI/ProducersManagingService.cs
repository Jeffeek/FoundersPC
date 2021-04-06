#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
    // todo: implement CRUD
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

        public async Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync(string managerToken)
        {
            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(GetProducerByIdAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentOutOfRangeException(nameof(managerToken));
            }

            using var client = _clientFactory.CreateClient("Producers getter client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<ProducerReadDto>>("Producers");

            return responseMessage;
        }

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

            using var client = _clientFactory.CreateClient("Producer getter client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.GetFromJsonAsync<ProducerReadDto>($"Producers/{id}");

            return responseMessage;
        }

        public async Task<bool> UpdateProducerAsync(int id, ProducerUpdateDto producer, string managerToken)
        {
            // todo: logger
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

                throw new ArgumentOutOfRangeException(nameof(managerToken));
            }

            using var client = _clientFactory.CreateClient("Update producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.PutAsJsonAsync($"Producers/{id}", producer);

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProducerAsync(int producerId, string managerToken)
        {
            // todo: logger
            if (producerId < 1)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(DeleteProducerAsync)}:{nameof(producerId)} was < 1");

                throw new ArgumentOutOfRangeException(nameof(producerId));
            }

            if (managerToken is null)
            {
                _logger.LogError($"{nameof(ProducersManagingService)}:{nameof(DeleteProducerAsync)}:{nameof(managerToken)} was null");

                throw new ArgumentOutOfRangeException(nameof(managerToken));
            }

            using var client = _clientFactory.CreateClient("Update producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        MicroservicesUrls.APIServer);

            var responseMessage = await client.DeleteAsync($"Producers/{producerId}");

            return responseMessage.IsSuccessStatusCode;
        }
    }
}