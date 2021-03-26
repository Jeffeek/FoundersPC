#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    public interface IUsersInformationWebService
    {
        Task<ApplicationUser> GetUserByIdAsync(int userId, string token);

        Task<ApplicationUser> GetUserByEmailAsync(string email, string token);

        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string token);
    }
}