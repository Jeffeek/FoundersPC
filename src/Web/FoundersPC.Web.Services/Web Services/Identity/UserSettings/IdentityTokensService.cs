#region Using namespaces

using System.Net.Http;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class IdentityTokensService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MicroservicesBaseAddresses _baseAddresses;

        public IdentityTokensService(IHttpClientFactory httpClientFactory, MicroservicesBaseAddresses baseAddresses)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
        }
    }
}