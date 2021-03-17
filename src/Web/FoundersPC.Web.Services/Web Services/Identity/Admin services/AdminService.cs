#region Using namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Blocking;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Unblocking;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Blocking;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    public class AdminService : IAdminService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AdminService> _logger;
        private readonly IUsersInformationService _usersInformationService;

        public AdminService(IUsersInformationService usersInformationService,
                            IHttpClientFactory clientFactory,
                            MicroservicesBaseAddresses baseAddresses,
                            ILogger<AdminService> logger
        )
        {
            _usersInformationService = usersInformationService;
            _clientFactory = clientFactory;
            _baseAddresses = baseAddresses;
            _logger = logger;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string adminToken) => await _usersInformationService.GetAll(adminToken);

        public async Task<ApplicationUser> GetUserByIdAsync(int id, string adminToken) => await _usersInformationService.GetByIdAsync(id, adminToken);

        public async Task<ApplicationUser> GetUserByEmailAsync(string email, string adminToken) =>
            await _usersInformationService.GetByEmailAsync(email, adminToken);

        public async Task<bool> BlockUserByIdAsync(int id, string adminToken)
        {
            if (id < 1)
            {
                _logger.LogWarning($"{nameof(AdminService)}: block user with id = {id}. Error");

                return false;
            }

            if (adminToken is null)
            {
                _logger.LogError($"{nameof(AdminService)}: block user: admin token was null.");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client = _clientFactory.CreateClient("Block user client");

            PrepareRequest(client, adminToken);

            var blockModel = new BlockUserByIdRequest
                             {
                                 BlockUserTokens = true,
                                 SendNotificationToUserViaEmail = true,
                                 UserId = id
                             };

            var blockUserRequest = await client.PutAsJsonAsync("Users/Block/By/Id", blockModel);

            if (!blockUserRequest.IsSuccessStatusCode) return false;

            var blockingResultModel = await blockUserRequest.Content.ReadFromJsonAsync<BlockUserResponse>();

            if (blockingResultModel is null)
            {
                _logger.LogError($"{nameof(AdminService)}: block user: {nameof(blockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(blockingResultModel));
            }

            if (blockingResultModel.IsBlockingSuccessful)
            {
                _logger.LogInformation($"{nameof(AdminService)}: block user: user with id = {id} was blocked by {blockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(AdminService)}: block user: user with id = {id} was not blocked by {blockingResultModel.AdministratorEmail}. Error = {blockingResultModel.Error}");

            return false;
        }

        public async Task<bool> UnblockUserByIdAsync(int id, string adminToken)
        {
            if (id < 1)
            {
                _logger.LogWarning($"{nameof(AdminService)}: unblock user with id = {id}. Error");

                return false;
            }

            if (adminToken is null)
            {
                _logger.LogError($"{nameof(AdminService)}: unblock user: admin token was null.");

                throw new ArgumentNullException(nameof(adminToken));
            }

            using var client = _clientFactory.CreateClient("Unblock user client");

            PrepareRequest(client, adminToken);

            var blockModel = new UnblockUserByIdRequest
                             {
                                 UnblockUserTokens = true,
                                 SendNotificationToUserViaEmail = true,
                                 UserId = id
                             };

            // todo: implement
            var unblockUserRequest = await client.PutAsJsonAsync("Users/Unblock/By/Id", blockModel);

            if (!unblockUserRequest.IsSuccessStatusCode) return false;

            var unblockingResultModel = await unblockUserRequest.Content.ReadFromJsonAsync<UnblockUserResponse>();

            if (unblockingResultModel is null)
            {
                _logger.LogError($"{nameof(AdminService)}: unblock user: {nameof(unblockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(unblockingResultModel));
            }

            if (unblockingResultModel.IsUnblockingSuccessful)
            {
                _logger.LogInformation($"{nameof(AdminService)}: unblock user: user with id = {id} was unblocked by {unblockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(AdminService)}: unblock user: user with id = {id} was not unblocked by {unblockingResultModel.AdministratorEmail}. Error = {unblockingResultModel.Error}");

            return false;
        }

        public Task<bool> BlockUserByEmailAsync(string email, string adminToken) => throw new NotImplementedException();

        public Task<bool> UnblockUserByEmailAsync(string email, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesAsync(string adminToken) => throw new NotImplementedException();

        public Task<ApplicationUserEntrance> GetEntranceByIdAsync(int id, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllUserEntrancesAsync(int userId, string adminToken) => throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesBetweenAsync(DateTime start, DateTime finish, string adminToken) =>
            throw new NotImplementedException();

        public Task<bool> RegisterNewManagerAsync(SignUpViewModel model, string adminToken) => throw new NotImplementedException();

        public Task<bool> RegisterNewManagerAsync(string email, string rawPassword, string adminToken) => throw new NotImplementedException();

        private void PrepareRequest(HttpClient client, string adminToken)
        {
            client.BaseAddress = new Uri($"{_baseAddresses.IdentityApiBaseAddress}Admin/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                                                                                       adminToken);
        }
    }
}