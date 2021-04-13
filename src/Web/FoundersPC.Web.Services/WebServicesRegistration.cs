﻿#region Using namespaces

using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Application.Interfaces.Services.Pricing;
using FoundersPC.Web.Services.Web_Services.HardwareAPI;
using FoundersPC.Web.Services.Web_Services.Identity.Admin_services;
using FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Tokens;
using FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Users;
using FoundersPC.Web.Services.Web_Services.Identity.Authentication;
using FoundersPC.Web.Services.Web_Services.Identity.Tokens;
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
            services.AddScoped<IProducersManagingService, ProducersManagingService>();
            services.AddScoped<ITokenReservationWebService, TokenReservationWebService>();
            services.AddScoped<IUsersEntrancesService, UsersEntrancesService>();
            services.AddScoped<IUsersAccessTokensLogsService, UsersAccessTokensLogsService>();
            services.AddScoped<IUserStatusService, UserStatusService>();
            services.AddScoped<IAuthenticationWebService, AuthenticationService>();
            services.AddScoped<IUserSettingsChangeWebService, UserSettingsChangeService>();
            services.AddScoped<IUsersInformationService, UsersInformationService>();
            services.AddScoped<IAdminService, AdminService>();
        }
    }
}