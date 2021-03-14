using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.WebIdentityShared;

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    public interface IUsersInformationService
    {
        Task<ApplicationUser> GetByIdAsync(int userId, string token);

        Task<ApplicationUser> GetByEmailAsync(string email, string token);

        Task<IEnumerable<ApplicationUser>> GetAll(string token);
    }
}
