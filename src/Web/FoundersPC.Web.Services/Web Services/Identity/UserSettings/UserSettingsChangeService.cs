#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.Response.ChangeSettings;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class UserSettingsChangeService : IUserSettingsChangeWebService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserSettingsChangeService> _logger;
        private readonly IMapper _mapper;

        public UserSettingsChangeService(IHttpClientFactory httpClientFactory,
                                         MicroservicesBaseAddresses baseAddresses,
                                         IMapper mapper,
                                         ILogger<UserSettingsChangeService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
            _mapper = mapper;
            _logger = logger;
        }

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

            using var client = _httpClientFactory.CreateClient("Change password client");
            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Users/SettingsChange/");

            var mappedModel = _mapper.Map<PasswordSettingsViewModel, ChangePasswordRequest>(model);

            var changePasswordRequest = await client.PutAsJsonAsync("Password",
                                                                    mappedModel);

            var responseContent =
                await changePasswordRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }

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

            using var client = _httpClientFactory.CreateClient("Change password client");
            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Users/SettingsChange/");

            var mappedModel = _mapper.Map<SecuritySettingsViewModel, ChangeLoginRequest>(model);

            var changeLoginRequest = await client.PutAsJsonAsync("Login",
                                                                 mappedModel);

            var responseContent = await changeLoginRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }

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

            using var client = _httpClientFactory.CreateClient("Change password client");
            client.PrepareJsonRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
                                                        token,
                                                        $"{_baseAddresses.IdentityApiBaseAddress}Users/SettingsChange/");

            var mappedModel = _mapper.Map<NotificationsSettingsViewModel, ChangeNotificationsRequest>(model);

            var changeNotificationsRequest = await client.PutAsJsonAsync("Notifications",
                                                                         mappedModel);

            var responseContent =
                await changeNotificationsRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }
    }
}