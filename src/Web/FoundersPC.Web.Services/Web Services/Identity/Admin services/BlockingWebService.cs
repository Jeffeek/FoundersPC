#region Using namespaces

using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Blocking;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Inactivity;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Unblocking;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Users.Blocking;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Users.Inactivity;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    public class BlockingWebService : IBlockingWebService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<BlockingWebService> _logger;

        public BlockingWebService(ILogger<BlockingWebService> logger,
                                  MicroservicesBaseAddresses baseAddresses,
                                  IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _baseAddresses = baseAddresses;
            _clientFactory = clientFactory;
        }

        public async Task<bool> BlockUserByIdAsync(int id, string adminToken)
        {
            if (id < 1)
            {
                _logger.LogWarning($"{nameof(BlockingWebService)}: block user with id = {id}. Error");

                return false;
            }

            using var client = _clientFactory.CreateClient("Block user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

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
                _logger.LogError($"{nameof(BlockingWebService)}: block user: {nameof(blockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(blockingResultModel));
            }

            if (blockingResultModel.IsBlockingSuccessful)
            {
                _logger.LogInformation($"{nameof(BlockingWebService)}: block user: user with id = {id} was blocked by {blockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(BlockingWebService)}: block user: user with id = {id} was not blocked by {blockingResultModel.AdministratorEmail}. Error = {blockingResultModel.Error}");

            return false;
        }

        public async Task<bool> UnblockUserByIdAsync(int id, string adminToken)
        {
            if (id < 1)
            {
                _logger.LogWarning($"{nameof(BlockingWebService)}: unblock user with id = {id}. Error");

                return false;
            }

            using var client = _clientFactory.CreateClient("Unblock user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

            var unblockModel = new UnblockUserByIdRequest
                               {
                                   UnblockUserTokens = true,
                                   SendNotificationToUserViaEmail = true,
                                   UserId = id
                               };

            var unblockUserRequest = await client.PutAsJsonAsync("Users/Unblock/By/Id", unblockModel);

            if (!unblockUserRequest.IsSuccessStatusCode) return false;

            var unblockingResultModel = await unblockUserRequest.Content.ReadFromJsonAsync<UnblockUserResponse>();

            if (unblockingResultModel is null)
            {
                _logger.LogError($"{nameof(BlockingWebService)}: unblock user: {nameof(unblockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(unblockingResultModel));
            }

            if (unblockingResultModel.IsUnblockingSuccessful)
            {
                _logger.LogInformation($"{nameof(BlockingWebService)}: unblock user: user with id = {id} was unblocked by {unblockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(BlockingWebService)}: unblock user: user with id = {id} was not unblocked by {unblockingResultModel.AdministratorEmail}. Error = {unblockingResultModel.Error}");

            return false;
        }

        public async Task<bool> BlockUserByEmailAsync(string email, string adminToken)
        {
            if (email is null)
            {
                _logger.LogWarning($"{nameof(BlockingWebService)}: block user with email = null. Error");

                return false;
            }

            using var client = _clientFactory.CreateClient("Block user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

            var blockModel = new BlockUserByEmailRequest
                             {
                                 BlockUserTokens = true,
                                 SendNotificationToUserViaEmail = true,
                                 UserEmail = email
                             };

            var blockUserRequest = await client.PutAsJsonAsync("Users/Block/By/Email", blockModel);

            if (!blockUserRequest.IsSuccessStatusCode) return false;

            var blockingResultModel = await blockUserRequest.Content.ReadFromJsonAsync<BlockUserResponse>();

            if (blockingResultModel is null)
            {
                _logger.LogError($"{nameof(BlockingWebService)}: block user: {nameof(blockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(blockingResultModel));
            }

            if (blockingResultModel.IsBlockingSuccessful)
            {
                _logger.LogInformation($"{nameof(BlockingWebService)}: block user: user with email = {email} was blocked by {blockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(BlockingWebService)}: block user: user with email = {email} was not blocked by {blockingResultModel.AdministratorEmail}. Error = {blockingResultModel.Error}");

            return false;
        }

        public async Task<bool> UnblockUserByEmailAsync(string email, string adminToken)
        {
            if (email is null)
            {
                _logger.LogWarning($"{nameof(AdminWebService)}: unblock user with email = null. Error");

                return false;
            }

            using var client = _clientFactory.CreateClient("Unblock user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

            var unblockModel = new UnblockUserByEmailRequest
                               {
                                   UnblockUserTokens = true,
                                   SendNotificationToUserViaEmail = true,
                                   UserEmail = email
                               };

            var unblockUserRequest = await client.PutAsJsonAsync("Users/Unblock/By/Email", unblockModel);

            if (!unblockUserRequest.IsSuccessStatusCode) return false;

            var unblockingResultModel = await unblockUserRequest.Content.ReadFromJsonAsync<UnblockUserResponse>();

            if (unblockingResultModel is null)
            {
                _logger.LogError($"{nameof(BlockingWebService)}: unblock user: {nameof(unblockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(unblockingResultModel));
            }

            if (unblockingResultModel.IsUnblockingSuccessful)
            {
                _logger.LogInformation($"{nameof(BlockingWebService)}: unblock user: user with email = {email} was unblocked by {unblockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(BlockingWebService)}: unblock user: user with email = {email} was not unblocked by {unblockingResultModel.AdministratorEmail}. Error = {unblockingResultModel.Error}");

            return false;
        }

        public async Task<bool> MakeUserInactiveByIdAsync(int id, string adminToken)
        {
            if (id < 1) return false;

            var client = _clientFactory.CreateClient("Make user inactive client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

            var requestModel = new MakeUserInactiveByIdRequest
                               {
                                   SendNotificationToUserViaEmail = true,
                                   UserId = id
                               };

            var request = await client.DeleteAsJsonAsync("Users/Inactive/By/Id", requestModel);

            if (request.IsSuccessStatusCode) return false;

            var contentResult = await request.Content.ReadFromJsonAsync<MakeUserInactiveResponse>();

            if (contentResult is null)
            {
                _logger.LogError($"{nameof(BlockingWebService)}: Make user inactive with id = {id}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (!contentResult.IsUserMadeInactiveSuccessful)
            {
                _logger.LogWarning($"{nameof(BlockingWebService)}: Make user inactive with id = {id}. Error = {contentResult.Error}");

                return false;
            }

            _logger.LogInformation($"{nameof(BlockingWebService)}: Make user inactive with id = {id}. Operation successful");

            return true;
        }

        public async Task<bool> MakeUserInactiveByEmailAsync(string email, string adminToken)
        {
            if (email is null) return false;

            var client = _clientFactory.CreateClient("Make user inactive client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Admin/");

            var requestModel = new MakeUserInactiveByEmailRequest
                               {
                                   SendNotificationToUserViaEmail = true,
                                   UserEmail = email
                               };

            var request = await client.DeleteAsJsonAsync("Inactive/By/Email", requestModel);

            if (request.IsSuccessStatusCode) return false;

            var contentResult = await request.Content.ReadFromJsonAsync<MakeUserInactiveResponse>();

            if (contentResult is null)
            {
                _logger.LogError($"{nameof(BlockingWebService)}: Make user inactive with email = {email}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (!contentResult.IsUserMadeInactiveSuccessful)
            {
                _logger.LogWarning($"{nameof(BlockingWebService)}: Make user inactive with email = {email}. Error = {contentResult.Error}");

                return false;
            }

            _logger.LogInformation($"{nameof(BlockingWebService)}: Make user inactive with email = {email}. Operation successful");

            return true;
        }
    }
}