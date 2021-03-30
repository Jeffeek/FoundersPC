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

            var client = _clientFactory.CreateClient("Producer getter client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        managerToken,
                                                        _baseAddresses.HardwareApiBaseAddress);

            var responseMessage = await client.GetFromJsonAsync<ProducerReadDto>($"Producers/{id}");

            return responseMessage;
        }
    }
}