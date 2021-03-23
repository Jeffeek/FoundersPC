#region Using namespaces

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Authentication
{
    public class IdentityAuthenticationService : IIdentityAuthenticationService
    {
        private readonly MicroservicesBaseAddresses _baseAddresses;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IdentityAuthenticationService> _logger;
        private readonly IMapper _mapper;

        public IdentityAuthenticationService(IHttpClientFactory httpClientFactory,
                                             MicroservicesBaseAddresses baseAddresses,
                                             IMapper mapper,
                                             ILogger<IdentityAuthenticationService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _baseAddresses = baseAddresses;
            _mapper = mapper;
            _logger = logger;
        }

        #region Sign In

        public async Task<UserLoginResponse> SignInAsync(string emailOrLogin, string rawPassword)
        {
            var signInModel = new SignInViewModel()
                              {
                                  LoginOrEmail = emailOrLogin,
                                  RawPassword = rawPassword
                              };

            return await SignInAsync(signInModel);
        }

        public async Task<UserLoginResponse> SignInAsync(SignInViewModel model)
        {
            if (model is null)
            {
                _logger.LogError($"{nameof(IdentityAuthenticationService)}: sign in: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (model.LoginOrEmail is null)
            {
                _logger.LogError($"{nameof(IdentityAuthenticationService)}: sign in: login or email was null");

                throw new ArgumentNullException(nameof(model.LoginOrEmail));
            }

            if (model.RawPassword is null)
            {
                _logger.LogError($"{nameof(IdentityAuthenticationService)}: sign in: raw password was null");

                throw new ArgumentNullException(nameof(model.RawPassword));
            }

            using var client = _httpClientFactory.CreateClient("Sign In client");
            client.PrepareJsonRequest($"{_baseAddresses.IdentityApiBaseAddress}Authentication/");

            var mappedRequestModel = _mapper.Map<SignInViewModel, UserSignInRequest>(model);

            var responseMessage = await client.PostAsJsonAsync<UserSignInRequest>("SignIn", mappedRequestModel);

            if (!responseMessage.IsSuccessStatusCode)
            {
                _logger.LogError($"Request from {nameof(IdentityAuthenticationService)} to Identity server returned a server error {responseMessage.StatusCode}");

                throw new AuthenticationException();
            }

            var signInResponseContent = await responseMessage.Content.ReadFromJsonAsync<UserLoginResponse>();

            return signInResponseContent;
        }

        #endregion

        #region Sign Up

        public async Task<UserSignUpResponse> SignUpAsync(string email, string rawPassword)
        {
            var signUpModel = new SignUpViewModel()
                              {
                                  Email = email,
                                  RawPassword = rawPassword
                              };

            return await SignUpAsync(signUpModel);
        }

        public async Task<UserSignUpResponse> SignUpAsync(SignUpViewModel model)
        {
            if (model is null)
            {
                _logger.LogError($"{nameof(IdentityAuthenticationService)}: sign up: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (model.Email is null)
            {
                _logger.LogError($"{nameof(IdentityAuthenticationService)}: sign up: email was null");

                throw new ArgumentNullException(nameof(model.Email));
            }

            if (model.RawPassword is null)
            {
                _logger.LogError($"{nameof(IdentityAuthenticationService)}: sign up: raw password was null");

                throw new ArgumentNullException(nameof(model.RawPassword));
            }

            var client = _httpClientFactory.CreateClient("Sign Up client");
            client.PrepareJsonRequest($"{_baseAddresses.IdentityApiBaseAddress}Authentication/");

            var mappedRequestModel = _mapper.Map<SignUpViewModel, UserSignUpRequest>(model);

            var signUpRequest = await client.PostAsJsonAsync("SignUp", mappedRequestModel);

            var signUpResponseContent = await signUpRequest.Content.ReadFromJsonAsync<UserSignUpResponse>();

            return signUpResponseContent;
        }

        #endregion

        #region Forgot password

        public async Task<UserForgotPasswordResponse> ForgotPasswordAsync(string email)
        {
            var forgotPasswordModel = new ForgotPasswordViewModel()
                                      {
                                          Email = email
                                      };

            return await ForgotPasswordAsync(forgotPasswordModel);
        }

        public async Task<UserForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            if (model is null)
            {
                _logger.LogError($"{nameof(IdentityAuthenticationService)}: forgot password: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (model.Email is null)
            {
                _logger.LogError($"{nameof(IdentityAuthenticationService)}: forgot password: email was null");

                throw new ArgumentNullException(nameof(model.Email));
            }

            var client = _httpClientFactory.CreateClient("Forgot password client");
            client.PrepareJsonRequest($"{_baseAddresses.IdentityApiBaseAddress}Authentication/");

            var mappedRequestModel = _mapper.Map<ForgotPasswordViewModel, UserForgotPasswordRequest>(model);

            var forgotPasswordRequest = await client.PostAsJsonAsync("ForgotPassword",
                                                                     mappedRequestModel);

            var forgotPasswordResponseContent =
                await forgotPasswordRequest.Content.ReadFromJsonAsync<UserForgotPasswordResponse>();

            return forgotPasswordResponseContent;
        }

        #endregion
    }
}