#region Using namespaces

using System.Net.Http;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class IdentityTokensService
    {
        private IHttpClientFactory _httpClientFactory;

        public IdentityTokensService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
    }
}