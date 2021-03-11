using System.Net.Http;

namespace FoundersPC.Web.Services.Web_Services.Identity.Authentication
{
    public class IdentityTokensService
    {
        private IHttpClientFactory _httpClientFactory;

        public IdentityTokensService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
