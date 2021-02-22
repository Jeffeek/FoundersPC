#region Using namespaces

using FoundersPC.Identity.Application.Interfaces.Services;
using FoundersPC.Identity.Services.User_Services;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Identity.Services
{
    public static class UsersIdentityServicesExtensions
    {
        public static void AddUsersIdentityServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordEncryptorService, PasswordEncryptorService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenEncryptorService, TokenEncryptorService>();
        }


    }
}