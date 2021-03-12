using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.Response.ChangeSettings;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FoundersPC.Web.Services.Web_Services.Identity.UserSettings
{
    public class IdentityUserSettingsChangeService : IIdentityUserSettingsChangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IMapper _mapper;

        public IdentityUserSettingsChangeService(IHttpClientFactory httpClientFactory, MicroservicesBaseAddresses baseAddresses, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
            _mapper = mapper;
        }

        private void PrepareRequest(HttpClient client,
                                    string token)
        {
            client.BaseAddress = new Uri(_baseAddresses.IdentityApiBaseAddress);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                                                                                       token);
        }

        public async Task<AccountSettingsChangeResponse> ChangePasswordAsync(PasswordSettingsViewModel model,
                                                                             string email,
                                                                             string token)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));
            if (model.OldPassword is null) throw new ArgumentNullException(nameof(model.OldPassword));
            if (model.NewPassword is null) throw new ArgumentNullException(nameof(model.NewPassword));
            if (model.NewPasswordConfirm is null) throw new ArgumentNullException(nameof(model.NewPasswordConfirm));
            if (token is null) throw new ArgumentNullException(nameof(token));
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("Change password client");
            PrepareRequest(client, token);

            var mappedModel = _mapper.Map<PasswordSettingsViewModel, ChangePasswordRequest>(model);

            mappedModel.Email = email;

            var changePasswordRequest = await client.PostAsJsonAsync<ChangePasswordRequest>("user/settings/change/password",
                                                                                            mappedModel);

            var responseContent = await changePasswordRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }

        public async Task<AccountSettingsChangeResponse> ChangeLoginAsync(SecuritySettingsViewModel model,
                                                                          string email,
                                                                          string token)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));
            if (model.NewLogin is null) throw new ArgumentNullException(nameof(model.NewLogin));
            if (token is null) throw new ArgumentNullException(nameof(token));
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("Change password client");
            PrepareRequest(client, token);

            var mappedModel = _mapper.Map<SecuritySettingsViewModel, ChangeLoginRequest>(model);

            mappedModel.Email = email;

            var changeLoginRequest = await client.PostAsJsonAsync<ChangeLoginRequest>("user/settings/change/login",
                                                                                      mappedModel);

            var responseContent = await changeLoginRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }

        public async Task<AccountSettingsChangeResponse> ChangeNotificationsAsync(NotificationsSettingsViewModel model, string email, string token)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));
            if (token is null) throw new ArgumentNullException(nameof(token));
            if (email is null) throw new ArgumentNullException(nameof(email));

            using var client = _httpClientFactory.CreateClient("Change password client");
            PrepareRequest(client, token);

            var mappedModel = _mapper.Map<NotificationsSettingsViewModel, ChangeNotificationsRequest>(model);
            mappedModel.Email = email;

            var changeNotificationsRequest = await client.PostAsJsonAsync<ChangeNotificationsRequest>("user/settings/change/notifications",
                                                                                                      mappedModel);

            var responseContent = await changeNotificationsRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            return responseContent;
        }
    }
}
