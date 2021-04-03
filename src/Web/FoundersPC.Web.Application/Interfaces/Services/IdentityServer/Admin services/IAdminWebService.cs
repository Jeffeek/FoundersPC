#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    /// <summary>
    ///     Interface for all admin possibilities
    /// </summary>
    public interface IAdminWebService
    {
        /// <summary>
        ///     Returns all users from database
        /// </summary>
        /// <param name="adminToken">JWT token</param>
        /// <returns></returns>
        Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync(string adminToken);

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

        /// <summary>
        ///     Gets all entrances to website
        /// </summary>
        /// <param name="adminToken">JWT token</param>
        /// <returns>Entrances</returns>
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken);

        /// <summary>
        ///     Gets entrance by id
        /// </summary>
        /// <param name="id">Id of entrance</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>User entrance for specific id; otherwise null</returns>
        Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken);

        /// <summary>
        ///     Gets all USER'S entrances
        /// </summary>
        /// <param name="userId">User's id which entrances to require</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>User's entrances; otherwise null</returns>
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesAsync(int userId, string adminToken);

        /// <summary>
        ///     Gets all entrances between <paramref name="start"/> and <paramref name="finish"/>
        /// </summary>
        /// <param name="start">Start date to grab</param>
        /// <param name="finish">Finish/end date to grab</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>Entrances between <paramref name="start"/> and <paramref name="finish"/></returns>
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesBetweenAsync(DateTime start,
                                                                              DateTime finish,
                                                                              string adminToken);

        /// <summary>
        ///     Register new manager with specific <paramref name="model"/>
        /// </summary>
        /// <param name="model">View model of Sign Up</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>Result of registration</returns>
        Task<bool> RegisterNewManagerAsync(SignUpViewModel model, string adminToken);

        /// <summary>
        ///     Register new manager with specific <paramref name="model"/>
        /// </summary>
        /// <param name="email">Email of new manager</param>
        /// <param name="rawPassword">Password of new manager</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>Result of registration</returns>
        Task<bool> RegisterNewManagerAsync(string email, string rawPassword, string adminToken);
    }
}