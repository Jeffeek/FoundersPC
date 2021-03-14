#region Using namespaces

using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Services.Web_Services;
using FoundersPC.Web.Services.Web_Services.HardwareAPI;
using FoundersPC.Web.Services.Web_Services.Identity;
using FoundersPC.Web.Services.Web_Services.Identity.Admin_services;
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
            services.AddSingleton(new MicroservicesBaseAddresses(configuration));
            services.AddScoped<IIdentityAuthenticationService, IdentityAuthenticationService>();
            services.AddScoped<IIdentityUserInformationService, IdentityUserInformationService>();
            services.AddScoped<IIdentityUserSettingsChangeService, IdentityUserSettingsChangeService>();
            services.AddScoped<IHardwareApiService, HardwareApiService>();
            services.AddScoped<IUsersInformationService, UsersInformationService>();
            services.AddScoped<IAdminService, AdminService>();
        }
    }
}