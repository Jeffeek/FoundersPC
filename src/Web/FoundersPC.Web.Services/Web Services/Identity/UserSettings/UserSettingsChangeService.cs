#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.IdentityServer.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.IdentityServer.Response.ChangeSettings;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Domain.Common.AccountSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class UserSettingsChangeService : IUserSettingsChangeWebService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserSettingsChangeService> _logger;
        private readonly IMapper _mapper;

        public UserSettingsChangeService(IHttpClientFactory httpClientFactory,
                                         IMapper mapper,
                                         ILogger<UserSettingsChangeService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _logger = logger;
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
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

        #endregion

        public async Task<AccountSettingsChangeResponse> ChangePasswordAsync(PasswordSettingsViewModel model,
                                                                             string token)
        {
            if (model is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change password: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (model.OldPassword is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change password: old password was null");

                throw new ArgumentNullException(nameof(model.OldPassword));
            }

            if (model.NewPassword is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change password: new password was null");

                throw new ArgumentNullException(nameof(model.NewPassword));
            }

            if (model.NewPasswordConfirm is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change password: new password confirm was null");

                throw new ArgumentNullException(nameof(model.NewPasswordConfirm));
            }

            if (token is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change password: token was null");

                throw new ArgumentNullException(nameof(token));
            }

            var client = _httpClientFactory.CreateClient("Change password client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        MicroservicesUrls.IdentityServer);

            var mappedModel = _mapper.Map<PasswordSettingsViewModel, ChangePasswordRequest>(model);

            var changePasswordRequest = await client.PutAsJsonAsync($"{IdentityServerRoutes.Users.SettingsChange.SettingsChangeEndpoint}/{IdentityServerRoutes.Users.SettingsChange.Password}",
                                                                    mappedModel);

            var responseContent =
                await changePasswordRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
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

        #endregion

        public async Task<AccountSettingsChangeResponse> ChangeLoginAsync(SecuritySettingsViewModel model,
                                                                          string token)
        {
            if (model is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change login: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (model.NewLogin is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change login: new login was null");

                throw new ArgumentNullException(nameof(model.NewLogin));
            }

            if (token is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change login: token was null");

                throw new ArgumentNullException(nameof(token));
            }

            var client = _httpClientFactory.CreateClient("Change password client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        MicroservicesUrls.IdentityServer);

            var mappedModel = _mapper.Map<SecuritySettingsViewModel, ChangeLoginRequest>(model);

            var changeLoginRequest = await client.PutAsJsonAsync($"{IdentityServerRoutes.Users.SettingsChange.SettingsChangeEndpoint}/{IdentityServerRoutes.Users.SettingsChange.Login}",
                                                                 mappedModel);

            var responseContent = await changeLoginRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="model"/> is <see langword="null"/></exception>
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

        #endregion

        public async Task<AccountSettingsChangeResponse> ChangeNotificationsAsync(NotificationsSettingsViewModel model,
                                                                                  string token)
        {
            if (model is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change notifications: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (token is null)
            {
                _logger.LogError($"{nameof(UserSettingsChangeService)}: change notifications: token was null");

                throw new ArgumentNullException(nameof(token));
            }

            var client = _httpClientFactory.CreateClient("Change password client");

            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        MicroservicesUrls.IdentityServer);

            var mappedModel = _mapper.Map<NotificationsSettingsViewModel, ChangeNotificationsRequest>(model);

            var changeNotificationsRequest = await client.PutAsJsonAsync($"{IdentityServerRoutes.Users.SettingsChange.SettingsChangeEndpoint}/{IdentityServerRoutes.Users.SettingsChange.Notifications}",
                                                                         mappedModel);

            var responseContent =
                await changeNotificationsRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }
    }
}