#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Headers;

#endregion

namespace FoundersPC.Web
{
    // make service for microservices
    internal static class ApplicationMicroservicesContext
    {
        internal static readonly HttpClient HardwareApiClient = new();

        internal static readonly HttpClient IdentityServerClient = new();

        static ApplicationMicroservicesContext()
        {
            IdentityServerClient.BaseAddress = new Uri("https://localhost:3000/");
            IdentityServerClient.DefaultRequestHeaders.Clear();
            IdentityServerClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IdentityServerClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            HardwareApiClient.BaseAddress = new Uri("https://localhost:9000/api/");
            HardwareApiClient.DefaultRequestHeaders.Clear();
            HardwareApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HardwareApiClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
    }
}