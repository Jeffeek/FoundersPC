#region Using namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users;
using FoundersPC.Web.Domain.Common.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    public class AdminService : IAdminService
    {
        public AdminService(IUsersInformationService usersInformationService,
                            IHttpClientFactory clientFactory,
                            ILogger<AdminService> logger,
                            IMapper mapper,
                            IUserStatusService userStatusService,
                            IUsersEntrancesService usersEntrancesService,
                            IUsersAccessTokensLogsService accessTokensLogsService)
        {
            _usersInformationService = usersInformationService;
            _clientFactory = clientFactory;
            _logger = logger;
            _mapper = mapper;
            _userStatusService = userStatusService;
            _usersEntrancesService = usersEntrancesService;
            _accessTokensLogsService = accessTokensLogsService;
        }

        #region DI

        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AdminService> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersInformationService _usersInformationService;
        private readonly IUserStatusService _userStatusService;
        private readonly IUsersEntrancesService _usersEntrancesService;
        private readonly IUsersAccessTokensLogsService _accessTokensLogsService;

        #endregion

        #region Users information

        public async Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync(string adminToken) => await _usersInformationService.GetAllUsersAsync(adminToken);

        /// <inheritdoc/>
        public Task<IEnumerable<UserEntityReadDto>>
            GetPaginateableUsersAsync(int pageNumber, int pageSize, string adminToken) =>
            _usersInformationService.GetPaginateableUsersAsync(pageNumber, pageSize, adminToken);

        public async Task<UserEntityReadDto> GetUserByIdAsync(int id, string adminToken) => await _usersInformationService.GetUserByIdAsync(id, adminToken);

        public async Task<UserEntityReadDto> GetUserByEmailAsync(string email, string adminToken) =>
            await _usersInformationService.GetUserByEmailAsync(email, adminToken);

        #endregion

        #region Block user

        public async Task<bool> BlockUserByIdAsync(int id, string adminToken) => await _userStatusService.BlockUserByIdAsync(id, adminToken);

        public async Task<bool> BlockUserByEmailAsync(string email, string adminToken) => await _userStatusService.BlockUserByEmailAsync(email, adminToken);

        #endregion

        #region Unblock user

        public async Task<bool> UnblockUserByIdAsync(int id, string adminToken) => await _userStatusService.UnblockUserByIdAsync(id, adminToken);

        public async Task<bool> UnblockUserByEmailAsync(string email, string adminToken) => await _userStatusService.UnblockUserByEmailAsync(email, adminToken);

        #endregion

        #region Make user inactive

        public async Task<bool> MakeUserInactiveByIdAsync(int id, string adminToken) => await _userStatusService.MakeUserInactiveByIdAsync(id, adminToken);

        public async Task<bool> MakeUserInactiveByEmailAsync(string email, string adminToken) =>
            await _userStatusService.MakeUserInactiveByEmailAsync(email, adminToken);

        #endregion

        #region Users entrances

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken) =>
            await _usersEntrancesService.GetAllEntrancesAsync(adminToken);

        /// <inheritdoc/>
        public async Task<IEnumerable<UserEntranceLogReadDto>>
            GetPaginateableEntrancesAsync(int pageNumber, int pageSize, string adminToken) =>
            await _usersEntrancesService.GetPaginateableEntrancesAsync(pageNumber, pageSize, adminToken);

        public async Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken) =>
            await _usersEntrancesService.GetEntranceByIdAsync(id, adminToken);

        public async Task<IEnumerable<UserEntranceLogReadDto>>
            GetAllUserEntrancesByIdAsync(int userId, string adminToken) =>
            await _usersEntrancesService.GetAllUserEntrancesByIdAsync(userId, adminToken);

        public async Task<IEnumerable<UserEntranceLogReadDto>>
            GetAllUserEntrancesByEmailAsync(string userEmail, string adminToken) =>
            await _usersEntrancesService.GetAllUserEntrancesByEmailAsync(userEmail, adminToken);

        public async Task<IEnumerable<UserEntranceLogReadDto>>
            GetAllEntrancesBetweenAsync(DateTime start, DateTime finish, string adminToken) =>
            await _usersEntrancesService.GetAllEntrancesBetweenAsync(start, finish, adminToken);

        #endregion

        #region Register new manager

        public async Task<bool> RegisterNewManagerAsync(SignUpViewModel model, string adminToken)
        {
            if (model is null)
                throw
                    new ArgumentNullException(nameof(model));

            if (model.Email is null)
                throw new ArgumentNullException(nameof(model.Email));

            if (model.RawPassword is null)
                throw new ArgumentNullException(nameof(model.RawPassword));

            if (model.RawPasswordConfirm is null)
                throw new ArgumentNullException(nameof(model.RawPasswordConfirm));

            if (!model.RawPassword.Equals(model.RawPasswordConfirm, StringComparison.Ordinal))
                throw new
                    ArgumentException($"{nameof(model.RawPassword)} was not equal to {nameof(model.RawPasswordConfirm)}");

            using var client = _clientFactory.CreateClient("Sign Up new manager client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var requestModel = _mapper.Map<SignUpViewModel, UserSignUpRequest>(model);

            var messageResponse = await client.PostAsJsonAsync("Admin/NewManager", requestModel);

            if (!messageResponse.IsSuccessStatusCode)
                return false;

            var messageContent = await messageResponse.Content.ReadFromJsonAsync<UserSignUpResponse>();

            if (messageContent is null)
            {
                _logger.LogError($"{nameof(AdminService)}: Register manager with email = {model.Email}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (messageContent.IsRegistrationSuccessful)
                return true;

            _logger.LogError($"{nameof(AdminService)}: Register manager with email = {model.Email}. Registration unsuccessful: {messageContent.ResponseException}");

            return false;
        }

        public async Task<bool> RegisterNewManagerAsync(string email, string rawPassword, string adminToken) =>
            await RegisterNewManagerAsync(new SignUpViewModel
                                          {
                                              Email = email,
                                              RawPassword = rawPassword,
                                              RawPasswordConfirm = rawPassword
                                          },
                                          adminToken);

        #endregion

        #region UsersAccessTokensLogs

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAllAccessTokensLogsAsync(string adminToken) =>
            await _accessTokensLogsService.GetAllAccessTokensLogsAsync(adminToken);

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>>
            GetPaginateableAccessTokensLogsAsync(int pageNumber, int pageSize, string adminToken) =>
            await _accessTokensLogsService.GetPaginateableAccessTokensLogsAsync(pageNumber, pageSize, adminToken);

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>>
            GetAllAccessTokensLogsByUserId(int userId, string adminToken) =>
            await _accessTokensLogsService.GetAllAccessTokensLogsByUserId(userId, adminToken);

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>>
            GetAllUserAccessTokensLogsByUserEmail(string userEmail, string adminToken) =>
            await _accessTokensLogsService.GetAllUserAccessTokensLogsByUserEmail(userEmail, adminToken);

        #endregion
    }
}