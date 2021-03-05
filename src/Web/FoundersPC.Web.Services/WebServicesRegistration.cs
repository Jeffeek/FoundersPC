using FoundersPC.Web.Services.Web_Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.Web.Services
{
    public static class WebServicesRegistration
    {
        public static void AddMicroservices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ApplicationMicroservices>(new ApplicationMicroservices(configuration["ConnectionServers:IdentityServer"],
                                                                                         configuration["ConnectionServers:API"]));
        }
    }
}