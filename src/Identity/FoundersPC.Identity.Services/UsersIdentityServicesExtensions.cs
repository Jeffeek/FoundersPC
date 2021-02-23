#region Using namespaces

using FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.Identity.Services.Log_Services;
using FoundersPC.Identity.Services.Token_Services;
using FoundersPC.Identity.Services.User_Services;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Identity.Services
{
    public static class UsersIdentityServicesExtensions
    {
        public static void AddUsersIdentityServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

        public static void AddEncryptionServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenEncryptorService, TokenEncryptorService>();
            services.AddTransient<IPasswordEncryptorService, PasswordEncryptorService>();
        }

        public static void AddLogsServices(this IServiceCollection services)
        {
            services.AddTransient<IAccessTokensLogsService, AccessTokensLogsService>();
            services.AddTransient<IUsersEntrancesService, UsersEntrancesService>();
        }

        public static void AddTokenServices(this IServiceCollection services)
        {
            services.AddTransient<IApiAccessUsersTokensService, ApiAccessUsersTokensService>();
        }
    }
}