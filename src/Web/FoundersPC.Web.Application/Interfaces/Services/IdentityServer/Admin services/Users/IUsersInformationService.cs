#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users
{
    public interface IUsersInformationService
    {
        /// <summary>
        ///     Returns user with specified identification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<UserEntityReadDto> GetUserByIdAsync(int id, string adminToken);

        /// <summary>
        ///     Returns user with specified identification
        /// </summary>
        /// <param name="email"></param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<UserEntityReadDto> GetUserByEmailAsync(string email, string adminToken);

        /// <summary>
        ///     Returns all users from database
        /// </summary>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync(string adminToken);

        /// <summary>
        ///     Returns users from database from <paramref name="pageNumber"/>
        /// </summary>
        /// <param name="pageNumber">Current page</param>
        /// <param name="pageSize">Max entities to show</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<IEnumerable<UserEntityReadDto>> GetPaginateableUsersAsync(int pageNumber, int pageSize, string adminToken);
    }
}