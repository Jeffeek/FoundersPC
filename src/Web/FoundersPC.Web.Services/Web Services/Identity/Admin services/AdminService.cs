using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.WebIdentityShared;

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    public class AdminService : IAdminService
    {
        private readonly IUsersInformationService _usersInformationService;

        public AdminService(IUsersInformationService usersInformationService)
        {
            _usersInformationService = usersInformationService;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string adminToken) => await _usersInformationService.GetAll(adminToken);

        public Task<ApplicationUser> GetUserByIdAsync(int id, string adminToken) => _usersInformationService.GetByIdAsync(id, adminToken);

        public Task<ApplicationUser> GetUserByEmailAsync(string email, string adminToken) => _usersInformationService.GetByEmailAsync(email, adminToken);

        public Task<bool> BlockUserByIdAsync(int id, string adminToken) => throw new NotImplementedException();

        public Task<bool> BlockUserByEmailAsync(string email, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesAsync(string adminToken) => throw new NotImplementedException();

        public Task<ApplicationUserEntrance> GetEntranceByIdAsync(int id, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllUserEntrancesAsync(int userId, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesBetweenAsync(DateTime start, DateTime finish, string adminToken) => throw new NotImplementedException();

        public Task<bool> RegisterNewManagerAsync(SignUpViewModel model, string adminToken) => throw new NotImplementedException();

        public Task<bool> RegisterNewManagerAsync(string email, string rawPassword, string adminToken) => throw new NotImplementedException();
    }
}
