#region Using namespaces

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Authentication
{
    public class IdentityAuthenticationService : IIdentityAuthenticationService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public IdentityAuthenticationService(IHttpClientFactory httpClientFactory,
                                             MicroservicesBaseAddresses baseAddresses,
                                             IMapper mapper
        )
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
            _mapper = mapper;
        }

        public async Task<UserLoginResponse> SignInAsync(string emailOrLogin, string rawPassword)
        {
            if (emailOrLogin is null) throw new ArgumentNullException(nameof(emailOrLogin));
            if (rawPassword is null) throw new ArgumentNullException(nameof(rawPassword));

            using var client = _httpClientFactory.CreateClient("Sign In client");
            PrepareRequest(client);

            var signInRequest = await client.PostAsJsonAsync("authentication/login",
                                                             new UserSignInRequest
                                                             {
                                                                 LoginOrEmail = emailOrLogin,
                                                                 Password = rawPassword
                                                             });

            var signInResponseContent = await signInRequest.Content.ReadFromJsonAsync<UserLoginResponse>();

            return signInResponseContent;
        }

        public async Task<UserLoginResponse> SignInAsync(SignInViewModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));
            if (model.LoginOrEmail is null) throw new ArgumentNullException(nameof(model.LoginOrEmail));
            if (model.RawPassword is null) throw new ArgumentNullException(nameof(model.RawPassword));

            using var client = _httpClientFactory.CreateClient("Sign In client");
            PrepareRequest(client);

            var mappedRequestModel = _mapper.Map<SignInViewModel, UserSignInRequest>(model);

            var signInRequest = await client.PostAsJsonAsync("authentication/login", mappedRequestModel);

            var signInResponseContent = await signInRequest.Content.ReadFromJsonAsync<UserLoginResponse>();

            return signInResponseContent;
        }

        public async Task<UserRegisterResponse> SignUpAsync(string email, string rawPassword)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));
            if (rawPassword is null) throw new ArgumentNullException(nameof(rawPassword));

            var client = _httpClientFactory.CreateClient("Sign Up client");
            PrepareRequest(client);

            var signUpRequest = await client.PostAsJsonAsync("authentication/registration",
                                                             new UserSignUpRequest
                                                             {
                                                                 Email = email,
                                                                 Password = rawPassword
                                                             });

            var signUpResponseContent = await signUpRequest.Content.ReadFromJsonAsync<UserRegisterResponse>();

            return signUpResponseContent;
        }

        public async Task<UserRegisterResponse> SignUpAsync(SignUpViewModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));
            if (model.Email is null) throw new ArgumentNullException(nameof(model.Email));
            if (model.RawPassword is null) throw new ArgumentNullException(nameof(model.RawPassword));

            var client = _httpClientFactory.CreateClient("Sign Up client");
            PrepareRequest(client);

            var mappedRequestModel = _mapper.Map<SignUpViewModel, UserSignUpRequest>(model);

            var signUpRequest = await client.PostAsJsonAsync("authentication/registration", mappedRequestModel);

            var signUpResponseContent = await signUpRequest.Content.ReadFromJsonAsync<UserRegisterResponse>();

            return signUpResponseContent;
        }

        public async Task<UserForgotPasswordResponse> ForgotPasswordAsync(string email)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));

            var client = _httpClientFactory.CreateClient("Forgot password client");
            PrepareRequest(client);

            var forgotPasswordRequest = await client.PostAsJsonAsync("authentication/forgotpassword",
                                                                     new UserForgotPasswordRequest
                                                                     {
                                                                         Email = email
                                                                     });

            var forgotPasswordResponseContent = await forgotPasswordRequest.Content.ReadFromJsonAsync<UserForgotPasswordResponse>();

            return forgotPasswordResponseContent;
        }

        public async Task<UserForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));
            if (model.Email is null) throw new ArgumentNullException(nameof(model.Email));

            var client = _httpClientFactory.CreateClient("Forgot password client");
            PrepareRequest(client);

            var mappedRequestModel = _mapper.Map<ForgotPasswordViewModel, UserForgotPasswordRequest>(model);

            var forgotPasswordRequest = await client.PostAsJsonAsync("authentication/forgotpassword",
                                                                     mappedRequestModel);

            var forgotPasswordResponseContent = await forgotPasswordRequest.Content.ReadFromJsonAsync<UserForgotPasswordResponse>();

            return forgotPasswordResponseContent;
        }

        private void PrepareRequest(HttpClient client)
        {
            client.BaseAddress = new Uri(_baseAddresses.IdentityApiBaseAddress);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
    }
}