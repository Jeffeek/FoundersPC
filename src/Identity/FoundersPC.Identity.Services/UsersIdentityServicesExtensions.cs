#region Using namespaces

using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Settings;
using FoundersPC.Identity.Services.Administration.Admin_Services;
using FoundersPC.Identity.Services.EmailServices;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.Identity.Services.Log_Services;
using FoundersPC.Identity.Services.Token_Services;
using FoundersPC.Identity.Services.User_Services;
using FoundersPC.Identity.Services.User_Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Identity.Services
{
    public static class UsersIdentityServicesExtensions
    {
        public static void AddUsersIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IUsersInformationService, UsersInformationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public static void AddAccessTokensServices(this IServiceCollection services)
        {
            services.AddScoped<IAccessTokensReservationService, AccessTokenReservationService>();
            services.AddScoped<IAccessTokensBlockingService, AccessTokensBlockingService>();
            services.AddScoped<IAccessTokensRequestsService, AccessTokensRequestsService>();
            services.AddScoped<IAccessTokensTokensStatusService, AccessTokensStatusService>();
            services.AddScoped<IAccessUsersTokensService, AccessUsersTokensService>();
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

        #region Docs

        /// <exception cref="T:System.OverflowException">
        ///     <paramref name="s"/> represents a number less than
        ///     <see cref="F:System.Int32.MinValue"/> or greater than <see cref="F:System.Int32.MaxValue"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="s"/> is not in the correct format.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Password:Key</exception>

        #endregion

        public static void AddBotEmailConfigurationAndService(this IServiceCollection services,
                                                              IConfiguration configuration)
        {
            services.AddSingleton(new EmailBotConfiguration(configuration));
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}