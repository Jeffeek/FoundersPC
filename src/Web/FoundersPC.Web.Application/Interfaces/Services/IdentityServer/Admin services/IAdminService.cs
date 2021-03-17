#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services
{
    // todo: get all token usages
    public interface IAdminService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string adminToken);

        Task<ApplicationUser> GetUserByIdAsync(int id, string adminToken);

        Task<ApplicationUser> GetUserByEmailAsync(string email, string adminToken);

        Task<bool> BlockUserByIdAsync(int id, string adminToken);

        Task<bool> BlockUserByEmailAsync(string email, string adminToken);

        Task<bool> UnblockUserByIdAsync(int id, string adminToken);

        Task<bool> UnblockUserByEmailAsync(string email, string adminToken);

        Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesAsync(string adminToken);

        Task<ApplicationUserEntrance> GetEntranceByIdAsync(int id, string adminToken);

        Task<IEnumerable<ApplicationUserEntrance>> GetAllUserEntrancesAsync(int userId, string adminToken);

        Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesBetweenAsync(DateTime start, DateTime finish, string adminToken);

        Task<bool> RegisterNewManagerAsync(SignUpViewModel model, string adminToken);

        Task<bool> RegisterNewManagerAsync(string email, string rawPassword, string adminToken);
    }
}