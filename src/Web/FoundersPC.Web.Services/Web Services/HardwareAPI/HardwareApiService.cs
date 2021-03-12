using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
    public class HardwareApiService : IHardwareApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly MicroservicesBaseAddresses _baseAddresses;
        
        public HardwareApiService(IHttpClientFactory clientFactory, MicroservicesBaseAddresses baseAddresses)
        {
            _clientFactory = clientFactory;
            _baseAddresses = baseAddresses;
        }

        private void PrepareRequest(HttpClient client, string token)
        {
            client.BaseAddress = new Uri(_baseAddresses.IdentityApiBaseAddress);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                                                                                       token);
        }
        
        public async Task<string> GetStringAsync(string entityType, string token)
        {
            if (entityType is null) throw new ArgumentNullException(nameof(entityType));
            if (token is null) throw new ArgumentNullException(nameof(token));

            using var client = _clientFactory.CreateClient();
            
            PrepareRequest(client, token);

            var request = await client.GetAsync(entityType);

            return await request.Content.ReadAsStringAsync();
        }
    }
}
