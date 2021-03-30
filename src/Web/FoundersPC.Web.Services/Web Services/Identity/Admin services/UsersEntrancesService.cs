#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        public Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken) => throw new NotImplementedException();

        public Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesBetweenAsync(DateTime start, DateTime finish, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByIdAsync(int userId, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesByEmailAsync(string userEmail, string adminToken) => throw new NotImplementedException();
    }
}