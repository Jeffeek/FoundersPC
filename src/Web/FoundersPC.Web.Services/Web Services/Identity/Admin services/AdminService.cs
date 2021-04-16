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
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Authentication;
using FoundersPC.RequestResponseShared.Pagination;
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
                            IUsersAccessTokensLogsService accessTokensLogsService,
                            IUsersAccessTokensService tokensService)
        {
            _usersInformationService = usersInformationService;
            _clientFactory = clientFactory;
            _logger = logger;
            _mapper = mapper;
            _userStatusService = userStatusService;
            _usersEntrancesService = usersEntrancesService;
            _accessTokensLogsService = accessTokensLogsService;
            _tokensService = tokensService;
        }

        #region DI

        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AdminService> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersInformationService _usersInformationService;
        private readonly IUserStatusService _userStatusService;
        private readonly IUsersEntrancesService _usersEntrancesService;
        private readonly IUsersAccessTokensLogsService _accessTokensLogsService;
        private readonly IUsersAccessTokensService _tokensService;

        #endregion

        #region Users information

        /// <inheritdoc/>
        public async Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync(string adminToken) => await _usersInformationService.GetAllUsersAsync(adminToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<UserEntityReadDto>>
            GetPaginateableUsersAsync(int pageNumber, int pageSize, string adminToken) =>
            _usersInformationService.GetPaginateableUsersAsync(pageNumber, pageSize, adminToken);

        /// <inheritdoc/>
        public Task<UserEntityReadDto> GetUserByIdAsync(int id, string adminToken) => _usersInformationService.GetUserByIdAsync(id, adminToken);

        /// <inheritdoc/>
        public Task<UserEntityReadDto> GetUserByEmailAsync(string email, string adminToken) =>
            _usersInformationService.GetUserByEmailAsync(email, adminToken);

        #endregion

        #region Block user

        /// <inheritdoc/>
        public Task<bool> BlockUserByIdAsync(int id, string adminToken) => _userStatusService.BlockUserByIdAsync(id, adminToken);

        /// <inheritdoc/>
        public Task<bool> BlockUserByEmailAsync(string email, string adminToken) => _userStatusService.BlockUserByEmailAsync(email, adminToken);

        #endregion

        #region Unblock user

        /// <inheritdoc/>
        public Task<bool> UnblockUserByIdAsync(int id, string adminToken) => _userStatusService.UnblockUserByIdAsync(id, adminToken);

        /// <inheritdoc/>
        public Task<bool> UnblockUserByEmailAsync(string email, string adminToken) => _userStatusService.UnblockUserByEmailAsync(email, adminToken);

        #endregion

        #region Make user inactive

        /// <inheritdoc/>
        public Task<bool> MakeUserInactiveByIdAsync(int id, string adminToken) => _userStatusService.MakeUserInactiveByIdAsync(id, adminToken);

        /// <inheritdoc/>
        public Task<bool> MakeUserInactiveByEmailAsync(string email, string adminToken) =>
            _userStatusService.MakeUserInactiveByEmailAsync(email, adminToken);

        #endregion

        #region Users entrances

        /// <inheritdoc/>
        public Task<IEnumerable<UserEntranceLogReadDto>> GetAllEntrancesAsync(string adminToken) =>
            _usersEntrancesService.GetAllEntrancesAsync(adminToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<UserEntranceLogReadDto>>
            GetPaginateableEntrancesAsync(int pageNumber, int pageSize, string adminToken) =>
            _usersEntrancesService.GetPaginateableEntrancesAsync(pageNumber, pageSize, adminToken);

        /// <inheritdoc/>
        public Task<UserEntranceLogReadDto> GetEntranceByIdAsync(int id, string adminToken) =>
            _usersEntrancesService.GetEntranceByIdAsync(id, adminToken);

        /// <inheritdoc/>
        public Task<IEnumerable<UserEntranceLogReadDto>>
            GetAllUserEntrancesByIdAsync(int userId, string adminToken) =>
            _usersEntrancesService.GetAllUserEntrancesByIdAsync(userId, adminToken);

        /// <inheritdoc/>
        public Task<IEnumerable<UserEntranceLogReadDto>>
            GetAllUserEntrancesByEmailAsync(string userEmail, string adminToken) =>
            _usersEntrancesService.GetAllUserEntrancesByEmailAsync(userEmail, adminToken);

        /// <inheritdoc/>
        public Task<IEnumerable<UserEntranceLogReadDto>>
            GetAllEntrancesBetweenAsync(DateTime start, DateTime finish, string adminToken) =>
            _usersEntrancesService.GetAllEntrancesBetweenAsync(start, finish, adminToken);

        #endregion

        #region Register new manager

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.ArgumentException">RawPassword != RawPasswordConfirm.</exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Response deserialize error.</exception>
        /// <exception cref="T:System.UriFormatException">Note: In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception,
        ///     <see cref="T:System.FormatException"/>, instead.
        ///     uriString is empty.
        ///     -or-
        ///     The scheme specified in uriString is not correctly formed. See
        ///     <see cref="M:System.Uri.CheckSchemeName(System.String)"/>.
        ///     -or-
        ///     uriString contains too many slashes.
        ///     -or-
        ///     The password specified in uriString is not valid.
        ///     -or-
        ///     The host name specified in uriString is not valid.
        ///     -or-
        ///     The file name specified in uriString is not valid.
        ///     -or-
        ///     The user name specified in uriString is not valid.
        ///     -or-
        ///     The host or authority name specified in uriString cannot be terminated by backslashes.
        ///     -or-
        ///     The port number specified in uriString is not valid or cannot be parsed.
        ///     -or-
        ///     The length of uriString exceeds 65519 characters.
        ///     -or-
        ///     The length of the scheme specified in uriString exceeds 1023 characters.
        ///     -or-
        ///     There is an invalid character sequence in uriString.
        ///     -or-
        ///     The MS-DOS path specified in uriString must start with c:\\.</exception>
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

            var client = _clientFactory.CreateClient("Sign Up new manager client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var requestModel = _mapper.Map<SignUpViewModel, UserSignUpRequest>(model);

            var messageResponse = await client.PostAsJsonAsync($"{IdentityServerRoutes.Authentication.Endpoint}/{IdentityServerRoutes.Authentication.SignUpManager}", requestModel);

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

        /// <inheritdoc/>
        /// <exception cref="T:System.Data.NoNullAllowedException">Response deserialize error.</exception>
        /// <exception cref="T:System.ArgumentException">RawPassword != RawPasswordConfirm.</exception>
        /// <exception cref="T:System.ArgumentNullException">model is <see langword="null"/></exception>
        public Task<bool> RegisterNewManagerAsync(string email, string rawPassword, string adminToken) =>
            RegisterNewManagerAsync(new SignUpViewModel
                                    {
                                        Email = email,
                                        RawPassword = rawPassword,
                                        RawPasswordConfirm = rawPassword
                                    },
                                    adminToken);

        #endregion

        #region UsersAccessTokensLogs

        /// <inheritdoc/>
        public Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsAsync(string adminToken) =>
            _accessTokensLogsService.GetAccessTokensLogsAsync(adminToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<AccessTokenLogReadDto>>
            GetPaginateableAccessTokensLogsAsync(int pageNumber, int pageSize, string adminToken) =>
            _accessTokensLogsService.GetPaginateableAccessTokensLogsAsync(pageNumber, pageSize, adminToken);

        /// <inheritdoc/>
        public Task<IEnumerable<AccessTokenLogReadDto>>
            GetAccessTokensLogsByUserIdAsync(int userId, string adminToken) =>
            _accessTokensLogsService.GetAccessTokensLogsByUserIdAsync(userId, adminToken);

        /// <inheritdoc/>
        public Task<IEnumerable<AccessTokenLogReadDto>>
            GetAccessTokensLogsByUserEmailAsync(string userEmail, string adminToken) =>
            _accessTokensLogsService.GetAccessTokensLogsByUserEmailAsync(userEmail, adminToken);

        /// <inheritdoc/>
        public Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByTokenIdAsync(int tokenId, string adminToken) =>
            _accessTokensLogsService.GetAccessTokensLogsByTokenIdAsync(tokenId, adminToken);

        /// <inheritdoc/>
        public Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByTokenAsync(string token, string adminToken) =>
            _accessTokensLogsService.GetAccessTokensLogsByTokenAsync(token, adminToken);

        #endregion

        #region UsersAccessTokensService

        /// <inheritdoc/>
        public Task<IEnumerable<AccessUserTokenReadDto>> GetAllUsersAccessTokensAsync(string adminToken) =>
            _tokensService.GetAllUsersAccessTokensAsync(adminToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<AccessUserTokenReadDto>> GetPaginateableTokensAsync(int pageNumber, int pageSize, string adminToken) =>
            _tokensService.GetPaginateableTokensAsync(pageNumber, pageSize, adminToken);

        /// <inheritdoc/>
        public Task<bool> BlockTokenByIdAsync(int tokenId, string adminToken) =>
            _tokensService.BlockTokenByIdAsync(tokenId, adminToken);

        /// <inheritdoc/>
        public Task<bool> BlockTokenByStringTokenAsync(string token, string adminToken) =>
            _tokensService.BlockTokenByStringTokenAsync(token, adminToken);

        #endregion
    }
}