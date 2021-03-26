#region Using namespaces

using System.Net.Http;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    // todo: interface
    public class TokensWebService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;

        public TokensWebService(IHttpClientFactory httpClientFactory, MicroservicesBaseAddresses baseAddresses)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
        }
    }
}