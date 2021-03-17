#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    public interface IUsersInformationService
    {
        Task<ApplicationUser> GetByIdAsync(int userId, string token);

        Task<ApplicationUser> GetByEmailAsync(string email, string token);

        Task<IEnumerable<ApplicationUser>> GetAll(string token);
    }
}