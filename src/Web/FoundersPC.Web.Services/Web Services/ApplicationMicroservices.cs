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
        public ApplicationMicroservices(string identityServerUri,
                                        string hardwareApiServerUri
        )
        {
            HardwareApiServer = new HttpClient
                                {
                                    BaseAddress = new Uri(hardwareApiServerUri)
                                };

            IdentityServer = new HttpClient
                             {
                                 BaseAddress = new Uri(identityServerUri)
                             };

            IdentityServer.DefaultRequestHeaders.Clear();
            IdentityServer.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IdentityServer.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            HardwareApiServer.DefaultRequestHeaders.Clear();
            HardwareApiServer.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HardwareApiServer.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }

        public HttpClient HardwareApiServer { get; }

        public HttpClient IdentityServer { get; }
    }
}