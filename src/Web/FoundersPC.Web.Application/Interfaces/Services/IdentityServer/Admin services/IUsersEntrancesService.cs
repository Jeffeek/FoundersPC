#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    public interface IUsersEntrancesService
    {
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken);

        Task<IEnumerable<UserEntranceLogReadDto>> GetPaginateableEntrancesAsync(int pageNumber,
            int pageSize,
            string adminToken);

        Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken);

        Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesBetweenAsync(DateTime start,
                                                                              DateTime finish,
                                                                              string adminToken);

        Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByIdAsync(int userId, string adminToken);

        Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByEmailAsync(string userEmail, string adminToken);
    }
}