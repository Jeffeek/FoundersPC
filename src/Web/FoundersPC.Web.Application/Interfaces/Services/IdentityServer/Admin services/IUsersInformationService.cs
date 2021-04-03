#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    public interface IUsersInformationService
    {
        Task<UserEntityReadDto> GetUserByIdAsync(int userId, string token);

        Task<UserEntityReadDto> GetUserByEmailAsync(string email, string token);

        Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync(string token);
    }
}