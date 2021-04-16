#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Pagination;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users
{
    public interface IUsersEntrancesService
    {
        /// <summary>
        ///     Gets all entrances to website
        /// </summary>
        /// <param name="adminToken">JWT token</param>
        /// <returns>Entrances</returns>
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken);

        /// <summary>
        ///     Gets entrances to website by pagination
        /// </summary>
        /// <param name="pageSize">Max size of page</param>
        /// <param name="adminToken">JWT token</param>
        /// <param name="pageNumber">Page to load</param>
        /// <returns>Entrances</returns>
        Task<IPaginationResponse<UserEntranceLogReadDto>> GetPaginateableEntrancesAsync(int pageNumber,
                                                                                        int pageSize,
                                                                                        string adminToken);

        /// <summary>
        ///     Gets entrance by id
        /// </summary>
        /// <param name="id">Id of entrance</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>User entrance for specific id; otherwise null</returns>
        Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken);

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
        ///     Gets all USER'S entrances
        /// </summary>
        /// <param name="userId">User's id which entrances to require</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>User's entrances; otherwise null</returns>
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByIdAsync(int userId, string adminToken);

        /// <summary>
        ///     Gets all USER'S entrances
        /// </summary>
        /// <param name="userEmail">User's email which entrances to require</param>
        /// <param name="adminToken">JWT token</param>
        /// <returns>User's entrances; otherwise null</returns>
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByEmailAsync(string userEmail, string adminToken);
    }
}