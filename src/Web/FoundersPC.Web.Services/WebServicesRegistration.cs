#region Using namespaces

using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Services.Web_Services;
using FoundersPC.Web.Services.Web_Services.Identity;
using FoundersPC.Web.Services.Web_Services.Identity.Authentication;
using FoundersPC.Web.Services.Web_Services.Identity.UserSettings;
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
            services.AddTransient<IIdentityUserSettingsChangeService, IdentityUserSettingsChangeService>();
        }
    }
}