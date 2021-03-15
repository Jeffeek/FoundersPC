#region Using namespaces

using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Settings;
using FoundersPC.Identity.Services.Administration.Admin_Services;
using FoundersPC.Identity.Services.Administration.Manager_Services;
using FoundersPC.Identity.Services.EmailServices;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.Identity.Services.Log_Services;
using FoundersPC.Identity.Services.Token_Services;
using FoundersPC.Identity.Services.User_Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Identity.Services
{
    public static class UsersIdentityServicesExtensions
    {
        public static void AddUsersIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            services.AddScoped<IUsersInformationService, UsersInformationService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IAdminService, AdminService>();
        }

        public static void AddEncryptionServices(this IServiceCollection services)
        {
            services.AddSingleton(new TokenEncryptorService());
            services.AddSingleton(new PasswordEncryptorService());
        }

        public static void AddLogsServices(this IServiceCollection services)
        {
            services.AddScoped<IAccessTokensLogsService, AccessTokensLogsService>();
            services.AddScoped<IUsersEntrancesService, UsersEntrancesService>();
        }

        public static void AddTokenServices(this IServiceCollection services)
        {
            services.AddScoped<IApiAccessUsersTokensService, ApiAccessUsersTokensService>();
        }

        public static void AddBotEmailConfigurationAndService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new EmailBotConfiguration(configuration));
            services.AddScoped<IMailService, MailService>();
        }
    }
}