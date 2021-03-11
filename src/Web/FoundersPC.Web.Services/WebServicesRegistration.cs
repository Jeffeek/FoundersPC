#region Using namespaces

using FoundersPC.Web.Services.Web_Services;
using FoundersPC.Web.Services.Web_Services.Identity;
using FoundersPC.Web.Services.Web_Services.Identity.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Web.Services
{
    public static class WebServicesRegistration
    {
        public static void AddMicroservices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MicroservicesBaseAddresses>(new MicroservicesBaseAddresses(configuration));
            services.AddTransient<IIdentityAuthenticationService, IdentityAuthenticationService>();
            services.AddTransient<IIdentityUserInformationService, IdentityUserInformationService>();
        }
    }
}