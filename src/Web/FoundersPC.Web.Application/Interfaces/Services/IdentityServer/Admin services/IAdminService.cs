#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users;
using FoundersPC.Web.Domain.Common.Authentication;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    /// <summary>
    ///     Interface for all admin possibilities
    /// </summary>
    public interface IAdminService : IUserStatusService,
                                     IUsersEntrancesService,
                                     IUsersInformationService,
                                     IUsersAccessTokensLogsService,
                                     IUsersAccessTokensService
    {
        /// <summary>
        ///     Register new manager with specific <paramref name="model"/>
        /// </summary>
        /// <param name="model">View model of Sign Up</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>Result of registration</returns>
        Task<bool> RegisterNewManagerAsync(SignUpViewModel model, string adminToken);

        /// <summary>
        ///     Register new manager with specific <paramref name="email"/> and <paramref name="rawPassword"/>
        /// </summary>
        /// <param name="email">Email of new manager</param>
        /// <param name="rawPassword">Password of new manager</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>Result of registration</returns>
        Task<bool> RegisterNewManagerAsync(string email, string rawPassword, string adminToken);
    }
}