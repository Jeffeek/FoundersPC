using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.WebIdentityShared;

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    public interface IUsersEntrancesService
    {
        Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesAsync(string adminToken);

        Task<ApplicationUserEntrance> GetEntranceByIdAsync(int id, string adminToken);

        Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesBetweenAsync(DateTime start,
                                                                               DateTime finish,
                                                                               string adminToken);

        Task<IEnumerable<ApplicationUserEntrance>> GetAllUserEntrancesByIdAsync(int userId, string adminToken);

        Task<IEnumerable<ApplicationUserEntrance>> GetAllUserEntrancesByEmailAsync(string userEmail, string adminToken);
    }
}
