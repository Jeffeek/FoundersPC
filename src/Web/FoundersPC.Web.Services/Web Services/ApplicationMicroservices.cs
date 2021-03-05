#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Headers;

#endregion

namespace FoundersPC.Web.Services.Web_Services
{
    // make service for microservices
    public class ApplicationMicroservices
    {
        public HttpClient HardwareApiClient { get; }

        public HttpClient IdentityServerClient { get; }

        public ApplicationMicroservices(string identityServerUri,
                                        string hardwareApiServerUri
        )
        {
            HardwareApiClient = new HttpClient
                                {
                                    BaseAddress = new Uri(hardwareApiServerUri)
                                };

            IdentityServerClient = new HttpClient
                                   {
                                       BaseAddress = new Uri(identityServerUri)
                                   };

            IdentityServerClient.DefaultRequestHeaders.Clear();
            IdentityServerClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IdentityServerClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            HardwareApiClient.DefaultRequestHeaders.Clear();
            HardwareApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HardwareApiClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
    }
}