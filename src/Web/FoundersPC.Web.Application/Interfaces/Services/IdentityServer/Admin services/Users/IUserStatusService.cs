#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users
{
    public interface IUserStatusService
    {
        /// <summary>
        ///     Blocks user with id = <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of user to block</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<bool> BlockUserByIdAsync(int id, string adminToken);

        /// <summary>
        ///     Blocks user with email = <paramref name="email"/>
        /// </summary>
        /// <param name="email">Email of user to block</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<bool> BlockUserByEmailAsync(string email, string adminToken);

        /// <summary>
        ///     Unblocks user with id = <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of user to unblock</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<bool> UnblockUserByIdAsync(int id, string adminToken);

        /// <summary>
        ///     Unblocks user with email = <paramref name="email"/>
        /// </summary>
        /// <param name="email">email of user to unblock</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<bool> UnblockUserByEmailAsync(string email, string adminToken);

        /// <summary>
        ///     Makes user inactive (removes from system) by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of user to remove</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<bool> MakeUserInactiveByIdAsync(int id, string adminToken);

        /// <summary>
        ///     Makes user inactive (removes from system) by <paramref name="email"/>
        /// </summary>
        /// <param name="email">Email of user to remove</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<bool> MakeUserInactiveByEmailAsync(string email, string adminToken);
    }
}