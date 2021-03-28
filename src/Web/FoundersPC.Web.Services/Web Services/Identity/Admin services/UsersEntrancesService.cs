#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        public Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesAsync(string adminToken) =>
            throw new NotImplementedException();

        public Task<ApplicationUserEntrance> GetEntranceByIdAsync(int id, string adminToken) =>
            throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesBetweenAsync(DateTime start,
            DateTime finish,
            string adminToken) =>
            throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllUserEntrancesByIdAsync(int userId, string adminToken) =>
            throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllUserEntrancesByEmailAsync(string userEmail,
            string adminToken) =>
            throw new NotImplementedException();
    }
}