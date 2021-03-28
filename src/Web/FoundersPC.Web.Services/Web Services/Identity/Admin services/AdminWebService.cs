#region Using namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services
{
    public class AdminWebService : IAdminWebService
    {
        public AdminWebService(IUsersInformationWebService usersInformationWebService,
                               IHttpClientFactory clientFactory,
                               MicroservicesBaseAddresses baseAddresses,
                               ILogger<AdminWebService> logger,
                               IMapper mapper,
                               IBlockingWebService blockingService,
                               IUsersEntrancesService usersEntrancesService)
        {
            _usersInformationWebService = usersInformationWebService;
            _clientFactory = clientFactory;
            _baseAddresses = baseAddresses;
            _logger = logger;
            _mapper = mapper;
            _blockingService = blockingService;
            _usersEntrancesService = usersEntrancesService;
        }

        #region DI

        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AdminWebService> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersInformationWebService _usersInformationWebService;
        private readonly IBlockingWebService _blockingService;
        private readonly IUsersEntrancesService _usersEntrancesService;

        #endregion

        #region Users information

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(string adminToken) =>
            await _usersInformationWebService.GetAllUsersAsync(adminToken);

        public async Task<ApplicationUser> GetUserByIdAsync(int id, string adminToken) =>
            await _usersInformationWebService.GetUserByIdAsync(id, adminToken);

        public async Task<ApplicationUser> GetUserByEmailAsync(string email, string adminToken) =>
            await _usersInformationWebService.GetUserByEmailAsync(email, adminToken);

        #endregion

        #region Block user

        public async Task<bool> BlockUserByIdAsync(int id, string adminToken) =>
            await _blockingService.BlockUserByIdAsync(id, adminToken);

        public async Task<bool> BlockUserByEmailAsync(string email, string adminToken) =>
            await _blockingService.BlockUserByEmailAsync(email, adminToken);

        #endregion

        #region Unblock user

        public async Task<bool> UnblockUserByIdAsync(int id, string adminToken) =>
            await _blockingService.UnblockUserByIdAsync(id, adminToken);

        public async Task<bool> UnblockUserByEmailAsync(string email, string adminToken) =>
            await _blockingService.UnblockUserByEmailAsync(email, adminToken);

        #endregion

        #region Make user inactive

        public async Task<bool> MakeUserInactiveByIdAsync(int id, string adminToken) =>
            await _blockingService.MakeUserInactiveByIdAsync(id, adminToken);

        public async Task<bool> MakeUserInactiveByEmailAsync(string email, string adminToken) =>
            await _blockingService.MakeUserInactiveByEmailAsync(email, adminToken);

        #endregion

        #region Users entrances

        public async Task<IEnumerable<ApplicationUserEntrance>> GetAllEntrancesAsync(string adminToken) =>
            await _usersEntrancesService.GetAllEntrancesAsync(adminToken);

        public async Task<ApplicationUserEntrance> GetEntranceByIdAsync(int id, string adminToken) =>
            await _usersEntrancesService.GetEntranceByIdAsync(id, adminToken);

        public async Task<IEnumerable<ApplicationUserEntrance>>
            GetAllUserEntrancesAsync(int userId, string adminToken) =>
            await _usersEntrancesService.GetAllUserEntrancesByIdAsync(userId, adminToken);

        public async Task<IEnumerable<ApplicationUserEntrance>>
            GetAllEntrancesBetweenAsync(DateTime start, DateTime finish, string adminToken) =>
            await _usersEntrancesService.GetAllEntrancesBetweenAsync(start, finish, adminToken);

        #endregion

        #region Register new manager

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
                _logger.LogError($"{nameof(AdminWebService)}: Register manager with email = {model.Email}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (messageContent.IsRegistrationSuccessful) return true;

            _logger.LogError($"{nameof(AdminWebService)}: Register manager with email = {model.Email}. Registration unsuccessful: {messageContent.ResponseException}");

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

        #endregion
    }
}