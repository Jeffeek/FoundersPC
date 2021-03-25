#region Using namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Blocking;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Inactivity;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Unblocking;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Blocking;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Inactivity;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    //todo: refactor this poop
    //todo: really.
    //todo: REFACTOR!
    public class AdminService : IAdminService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AdminService> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersInformationService _usersInformationService;

        public AdminService(IUsersInformationService usersInformationService,
                            IHttpClientFactory clientFactory,
                            MicroservicesBaseAddresses baseAddresses,
                            ILogger<AdminService> logger,
                            IMapper mapper)
        {
            _usersInformationService = usersInformationService;
            _clientFactory = clientFactory;
            _baseAddresses = baseAddresses;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string adminToken) =>
            await _usersInformationService.GetAll(adminToken);

        public async Task<ApplicationUser> GetUserByIdAsync(int id, string adminToken) =>
            await _usersInformationService.GetByIdAsync(id, adminToken);

        public async Task<ApplicationUser> GetUserByEmailAsync(string email, string adminToken) =>
            await _usersInformationService.GetByEmailAsync(email, adminToken);

        public async Task<bool> BlockUserByIdAsync(int id, string adminToken)
        {
            if (id < 1)
            {
                _logger.LogWarning($"{nameof(AdminService)}: block user with id = {id}. Error");

                return false;
            }

            using var client = _clientFactory.CreateClient("Block user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        _baseAddresses.IdentityApiBaseAddress);

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

            using var client = _clientFactory.CreateClient("Unblock user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        _baseAddresses.IdentityApiBaseAddress);

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

        public async Task<bool> BlockUserByEmailAsync(string email, string adminToken)
        {
            if (email is null)
            {
                _logger.LogWarning($"{nameof(AdminService)}: block user with email = null. Error");

                return false;
            }

            using var client = _clientFactory.CreateClient("Block user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        _baseAddresses.IdentityApiBaseAddress);

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
                _logger.LogError($"{nameof(AdminService)}: block user: {nameof(blockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(blockingResultModel));
            }

            if (blockingResultModel.IsBlockingSuccessful)
            {
                _logger.LogInformation($"{nameof(AdminService)}: block user: user with email = {email} was blocked by {blockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(AdminService)}: block user: user with email = {email} was not blocked by {blockingResultModel.AdministratorEmail}. Error = {blockingResultModel.Error}");

            return false;
        }

        public async Task<bool> UnblockUserByEmailAsync(string email, string adminToken)
        {
            if (email is null)
            {
                _logger.LogWarning($"{nameof(AdminService)}: unblock user with email = null. Error");

                return false;
            }

            using var client = _clientFactory.CreateClient("Unblock user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        _baseAddresses.IdentityApiBaseAddress);

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
                _logger.LogError($"{nameof(AdminService)}: unblock user: {nameof(unblockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(unblockingResultModel));
            }

            if (unblockingResultModel.IsUnblockingSuccessful)
            {
                _logger.LogInformation($"{nameof(AdminService)}: unblock user: user with email = {email} was unblocked by {unblockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(AdminService)}: unblock user: user with email = {email} was not unblocked by {unblockingResultModel.AdministratorEmail}. Error = {unblockingResultModel.Error}");

            return false;
        }

        public async Task<bool> MakeUserInactiveByIdAsync(int id, string adminToken)
        {
            if (id < 1) return false;

            var client = _clientFactory.CreateClient("Make user inactive client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        _baseAddresses.IdentityApiBaseAddress);

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
                _logger.LogError($"{nameof(AdminService)}: Make user inactive with id = {id}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (!contentResult.IsUserMadeInactiveSuccessful)
            {
                _logger.LogWarning($"{nameof(AdminService)}: Make user inactive with id = {id}. Error = {contentResult.Error}");

                return false;
            }

            _logger.LogInformation($"{nameof(AdminService)}: Make user inactive with id = {id}. Operation successful");

            return true;
        }

        public async Task<bool> MakeUserInactiveByEmailAsync(string email, string adminToken)
        {
            if (email is null) return false;

            var client = _clientFactory.CreateClient("Make user inactive client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        _baseAddresses.IdentityApiBaseAddress);

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
                _logger.LogError($"{nameof(AdminService)}: Make user inactive with email = {email}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (!contentResult.IsUserMadeInactiveSuccessful)
            {
                _logger.LogWarning($"{nameof(AdminService)}: Make user inactive with email = {email}. Error = {contentResult.Error}");

                return false;
            }

            _logger.LogInformation($"{nameof(AdminService)}: Make user inactive with email = {email}. Operation successful");

            return true;
        }

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesAsync(string adminToken) =>
            throw new NotImplementedException();

        public Task<ApplicationUserEntrance> GetEntranceByIdAsync(int id, string adminToken) =>
            throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllUserEntrancesAsync(int userId, string adminToken) =>
            throw new NotImplementedException();

        public Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesBetweenAsync(DateTime start,
            DateTime finish,
            string adminToken) =>
            throw new NotImplementedException();

        public async Task<bool> RegisterNewManagerAsync(SignUpViewModel model, string adminToken)
        {
            if (model is null)
                throw
                    new ArgumentNullException(nameof(model));
            if (model.Email is null) throw new ArgumentNullException(nameof(model.Email));
            if (model.RawPassword is null) throw new ArgumentNullException(nameof(model.RawPassword));
            if (model.RawPasswordConfirm is null) throw new ArgumentNullException(nameof(model.RawPasswordConfirm));
            if (!model.RawPassword.Equals(model.RawPasswordConfirm, StringComparison.Ordinal))
                throw new
                    ArgumentException($"{nameof(model.RawPassword)} was not equal to {nameof(model.RawPasswordConfirm)}");

            var client = _clientFactory.CreateClient("Sign Up new manager client");
            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        _baseAddresses.IdentityApiBaseAddress);

            var requestModel = _mapper.Map<SignUpViewModel, UserSignUpRequest>(model);

            var messageResponse = await client.PostAsJsonAsync("Admin/NewManager", requestModel);

            if (!messageResponse.IsSuccessStatusCode) return false;

            var messageContent = await messageResponse.Content.ReadFromJsonAsync<UserSignUpResponse>();

            if (messageContent is null)
            {
                _logger.LogError($"{nameof(AdminService)}: Register manager with email = {model.Email}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (messageContent.IsRegistrationSuccessful) return true;

            _logger.LogError($"{nameof(AdminService)}: Register manager with email = {model.Email}. Registration unsuccessful: {messageContent.ResponseException}");

            return false;
        }

        public Task<bool> RegisterNewManagerAsync(string email, string rawPassword, string adminToken) =>
            RegisterNewManagerAsync(new SignUpViewModel
                                    {
                                        Email = email,
                                        RawPassword = rawPassword,
                                        RawPasswordConfirm = rawPassword
                                    },
                                    adminToken);
    }
}