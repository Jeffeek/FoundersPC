#region Using namespaces

using FoundersPC.Web.Services.Web_Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Web.Services
{
    public static class WebServicesRegistration
    {
        public static void AddMicroservices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(provider => new ApplicationMicroservices(configuration["ConnectionServers:IdentityServer"],
                                                                           configuration["ConnectionServers:API"]));
        }
    }
}