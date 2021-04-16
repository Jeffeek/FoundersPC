#region Using namespaces

using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Blocking;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Inactivity;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Unblocking;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Administration.Admin.Users.Blocking;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Administration.Admin.Users.Inactivity;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Admin_services.Users
{
    public class UserStatusService : IUserStatusService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<UserStatusService> _logger;

        public UserStatusService(ILogger<UserStatusService> logger,
                                 IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        #region Docs

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
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">User was null after parsing.</exception>

        #endregion

        public async Task<bool> BlockUserByIdAsync(int id, string adminToken)
        {
            if (id < 1)
            {
                _logger.LogWarning($"{nameof(UserStatusService)}: block user with id = {id}. Error");

                return false;
            }

            var client = _clientFactory.CreateClient("Block user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var blockModel = new BlockUserByIdRequest
                             {
                                 BlockUserTokens = true,
                                 SendNotificationToUserViaEmail = true,
                                 UserId = id
                             };

            var blockUserRequest = await client.PutAsJsonAsync($"{IdentityServerRoutes.Users.Status.UserChangeStatusEndpoint}/{IdentityServerRoutes.Users.Status.Block.ByUserId}", blockModel);

            if (!blockUserRequest.IsSuccessStatusCode)
                return false;

            var blockingResultModel = await blockUserRequest.Content.ReadFromJsonAsync<BlockUserResponse>();

            if (blockingResultModel is null)
            {
                _logger.LogError($"{nameof(UserStatusService)}: block user: {nameof(blockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(blockingResultModel));
            }

            if (blockingResultModel.IsBlockingSuccessful)
            {
                _logger.LogInformation($"{nameof(UserStatusService)}: block user: user with id = {id} was blocked by {blockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(UserStatusService)}: block user: user with id = {id} was not blocked by {blockingResultModel.AdministratorEmail}. Error = {blockingResultModel.Error}");

            return false;
        }

        #region Docs

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
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Blocking result parsing was null.</exception>

        #endregion

        public async Task<bool> BlockUserByEmailAsync(string email, string adminToken)
        {
            if (email is null)
            {
                _logger.LogWarning($"{nameof(UserStatusService)}: block user with email = null. Error");

                return false;
            }

            var client = _clientFactory.CreateClient("Block user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var blockModel = new BlockUserByEmailRequest
                             {
                                 BlockUserTokens = true,
                                 SendNotificationToUserViaEmail = true,
                                 UserEmail = email
                             };

            var blockUserRequest = await client.PutAsJsonAsync($"{IdentityServerRoutes.Users.Status.UserChangeStatusEndpoint}/{IdentityServerRoutes.Users.Status.Block.ByUserEmail}", blockModel);

            if (!blockUserRequest.IsSuccessStatusCode)
                return false;

            var blockingResultModel = await blockUserRequest.Content.ReadFromJsonAsync<BlockUserResponse>();

            if (blockingResultModel is null)
            {
                _logger.LogError($"{nameof(UserStatusService)}: block user: {nameof(blockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(blockingResultModel));
            }

            if (blockingResultModel.IsBlockingSuccessful)
            {
                _logger.LogInformation($"{nameof(UserStatusService)}: block user: user with email = {email} was blocked by {blockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(UserStatusService)}: block user: user with email = {email} was not blocked by {blockingResultModel.AdministratorEmail}. Error = {blockingResultModel.Error}");

            return false;
        }

        #region Docs

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
        /// <exception cref="T:System.Data.NoNullAllowedException">Parsing error.</exception>
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>

        #endregion

        public async Task<bool> UnblockUserByIdAsync(int id, string adminToken)
        {
            if (id < 1)
            {
                _logger.LogWarning($"{nameof(UserStatusService)}: unblock user with id = {id}. Error");

                return false;
            }

            var client = _clientFactory.CreateClient("Unblock user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var unblockModel = new UnblockUserByIdRequest
                               {
                                   UnblockUserTokens = true,
                                   SendNotificationToUserViaEmail = true,
                                   UserId = id
                               };

            var unblockUserRequest = await client.PutAsJsonAsync($"{IdentityServerRoutes.Users.Status.UserChangeStatusEndpoint}/{IdentityServerRoutes.Users.Status.Unblock.ByUserId}", unblockModel);

            if (!unblockUserRequest.IsSuccessStatusCode)
                return false;

            var unblockingResultModel = await unblockUserRequest.Content.ReadFromJsonAsync<UnblockUserResponse>();

            if (unblockingResultModel is null)
            {
                _logger.LogError($"{nameof(UserStatusService)}: unblock user: {nameof(unblockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(unblockingResultModel));
            }

            if (unblockingResultModel.IsUnblockingSuccessful)
            {
                _logger.LogInformation($"{nameof(UserStatusService)}: unblock user: user with id = {id} was unblocked by {unblockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(UserStatusService)}: unblock user: user with id = {id} was not unblocked by {unblockingResultModel.AdministratorEmail}. Error = {unblockingResultModel.Error}");

            return false;
        }

        #region Docs

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
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Parsing error.</exception>

        #endregion

        public async Task<bool> UnblockUserByEmailAsync(string email, string adminToken)
        {
            if (email is null)
            {
                _logger.LogWarning($"{nameof(AdminService)}: unblock user with email = null. Error");

                return false;
            }

            var client = _clientFactory.CreateClient("Unblock user client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var unblockModel = new UnblockUserByEmailRequest
                               {
                                   UnblockUserTokens = true,
                                   SendNotificationToUserViaEmail = true,
                                   UserEmail = email
                               };

            var unblockUserRequest = await client.PutAsJsonAsync($"{IdentityServerRoutes.Users.Status.UserChangeStatusEndpoint}/{IdentityServerRoutes.Users.Status.Unblock.ByUserEmail}", unblockModel);

            if (!unblockUserRequest.IsSuccessStatusCode)
                return false;

            var unblockingResultModel = await unblockUserRequest.Content.ReadFromJsonAsync<UnblockUserResponse>();

            if (unblockingResultModel is null)
            {
                _logger.LogError($"{nameof(UserStatusService)}: unblock user: {nameof(unblockingResultModel)} was null after parsing");

                throw new NoNullAllowedException(nameof(unblockingResultModel));
            }

            if (unblockingResultModel.IsUnblockingSuccessful)
            {
                _logger.LogInformation($"{nameof(UserStatusService)}: unblock user: user with email = {email} was unblocked by {unblockingResultModel.AdministratorEmail}");

                return true;
            }

            _logger.LogWarning($"{nameof(UserStatusService)}: unblock user: user with email = {email} was not unblocked by {unblockingResultModel.AdministratorEmail}. Error = {unblockingResultModel.Error}");

            return false;
        }

        #region Docs

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
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">.NET Core and .NET 5.0 and later only: The request failed due to timeout.</exception>
        /// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Parsing error.</exception>

        #endregion

        public async Task<bool> MakeUserInactiveByIdAsync(int id, string adminToken)
        {
            if (id < 1)
                return false;

            var client = _clientFactory.CreateClient("Make user inactive client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var requestModel = new MakeUserInactiveByIdRequest
                               {
                                   SendNotificationToUserViaEmail = true,
                                   UserId = id
                               };

            var request = await client.DeleteAsJsonAsync($"{IdentityServerRoutes.Users.Status.UserChangeStatusEndpoint}/{IdentityServerRoutes.Users.Status.MakeInactive.ByUserId}", requestModel);

            if (request.IsSuccessStatusCode)
                return false;

            var contentResult = await request.Content.ReadFromJsonAsync<MakeUserInactiveResponse>();

            if (contentResult is null)
            {
                _logger.LogError($"{nameof(UserStatusService)}: Make user inactive with id = {id}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (!contentResult.IsUserMadeInactiveSuccessful)
            {
                _logger.LogWarning($"{nameof(UserStatusService)}: Make user inactive with id = {id}. Error = {contentResult.Error}");

                return false;
            }

            _logger.LogInformation($"{nameof(UserStatusService)}: Make user inactive with id = {id}. Operation successful");

            return true;
        }

        #region Docs

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
        /// <exception cref="T:System.ArgumentNullException">uriString is <see langword="null"/>.</exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Parsing error.</exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        /// <exception cref="T:System.Threading.Tasks.TaskCanceledException">.NET Core and .NET 5.0 and later only: The request failed due to timeout.</exception>
        /// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>

        #endregion

        public async Task<bool> MakeUserInactiveByEmailAsync(string email, string adminToken)
        {
            if (email is null)
                return false;

            var client = _clientFactory.CreateClient("Make user inactive client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        adminToken,
                                                        MicroservicesUrls.IdentityServer);

            var requestModel = new MakeUserInactiveByEmailRequest
                               {
                                   SendNotificationToUserViaEmail = true,
                                   UserEmail = email
                               };

            var request = await client.DeleteAsJsonAsync($"{IdentityServerRoutes.Users.Status.UserChangeStatusEndpoint}/{IdentityServerRoutes.Users.Status.MakeInactive.ByUserEmail}", requestModel);

            if (request.IsSuccessStatusCode)
                return false;

            var contentResult = await request.Content.ReadFromJsonAsync<MakeUserInactiveResponse>();

            if (contentResult is null)
            {
                _logger.LogError($"{nameof(UserStatusService)}: Make user inactive with email = {email}. Response deserialize error");

                throw new NoNullAllowedException();
            }

            if (!contentResult.IsUserMadeInactiveSuccessful)
            {
                _logger.LogWarning($"{nameof(UserStatusService)}: Make user inactive with email = {email}. Error = {contentResult.Error}");

                return false;
            }

            _logger.LogInformation($"{nameof(UserStatusService)}: Make user inactive with email = {email}. Operation successful");

            return true;
        }
    }
}