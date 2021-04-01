#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;

#endregion

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
    // todo: implement CRUD
    public class ProducersManagingService : IProducersManagingService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _clientFactory;

        public ProducersManagingService(IHttpClientFactory clientFactory,
                                        MicroservicesBaseAddresses baseAddresses)
        {
            _clientFactory = clientFactory;
            _baseAddresses = baseAddresses;
        }

        public async Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync(string managerToken)
        {
            if (managerToken is null) throw new ArgumentNullException(nameof(managerToken));

            using var client = _clientFactory.CreateClient("Producers getter client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        _baseAddresses.HardwareApiBaseAddress);

            var responseMessage = await client.GetFromJsonAsync<IEnumerable<ProducerReadDto>>("Producers");

            return responseMessage;
        }

        public async Task<ProducerReadDto> GetProducerByIdAsync(int id, string managerToken)
        {
            if (managerToken is null) throw new ArgumentNullException(nameof(managerToken));

            using var client = _clientFactory.CreateClient("Producer getter client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        _baseAddresses.HardwareApiBaseAddress);

            var responseMessage = await client.GetFromJsonAsync<ProducerReadDto>($"Producers/{id}");

            return responseMessage;
        }

        public async Task<bool> UpdateProducerAsync(int id, ProducerUpdateDto producer, string managerToken)
        {
            // todo: logger
            if (producer is null) throw new ArgumentNullException(nameof(producer));
            if (id < 1) throw new ArgumentOutOfRangeException(nameof(id));

            using var client = _clientFactory.CreateClient("Update producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme, managerToken, _baseAddresses.HardwareApiBaseAddress);

            var responseMessage = await client.PutAsJsonAsync<ProducerUpdateDto>($"Producers/{id}", producer);

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProducerAsync(int producerId, string managerToken)
        {
            // todo: logger
            if (producerId < 1) throw new ArgumentOutOfRangeException(nameof(producerId));

            using var client = _clientFactory.CreateClient("Update producer client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme, managerToken, _baseAddresses.HardwareApiBaseAddress);

            var responseMessage = await client.DeleteAsync($"Producers/{producerId}");

            return responseMessage.IsSuccessStatusCode;
        }
    }
}