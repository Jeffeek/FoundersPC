#region Using namespaces

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.Identity.Authentication
{
    public class AuthenticationService : IAuthenticationWebService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;

        public AuthenticationService(IHttpClientFactory httpClientFactory,
                                     IMapper mapper,
                                     ILogger<AuthenticationService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _logger = logger;
        }

        #region Sign In

        public async Task<UserLoginResponse> SignInAsync(string emailOrLogin, string rawPassword)
        {
            var signInModel = new SignInViewModel
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
                _logger.LogError($"{nameof(AuthenticationService)}: sign in: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (model.LoginOrEmail is null)
            {
                _logger.LogError($"{nameof(AuthenticationService)}: sign in: login or email was null");

                throw new ArgumentNullException(nameof(model.LoginOrEmail));
            }

            if (model.RawPassword is null)
            {
                _logger.LogError($"{nameof(AuthenticationService)}: sign in: raw password was null");

                throw new ArgumentNullException(nameof(model.RawPassword));
            }

            using var client = _httpClientFactory.CreateClient("Sign In client");
            client.PrepareJsonRequest($"{MicroservicesUrls.IdentityServer}Authentication/");

            var mappedRequestModel = _mapper.Map<SignInViewModel, UserSignInRequest>(model);

            var responseMessage = await client.PostAsJsonAsync("SignIn", mappedRequestModel);

            if (!responseMessage.IsSuccessStatusCode)
            {
                _logger.LogError($"Request from {nameof(AuthenticationService)}, Sign In: to Identity server returned a server error : StatusCode: {responseMessage.StatusCode}");

                if (responseMessage.StatusCode == HttpStatusCode.UnprocessableEntity)
                    throw new NetworkInformationException((int)responseMessage.StatusCode);
            }

            var signInResponseContent = await responseMessage.Content.ReadFromJsonAsync<UserLoginResponse>();

            return signInResponseContent;
        }

        #endregion

        #region Sign Up

        public async Task<UserSignUpResponse> SignUpAsync(string email, string rawPassword)
        {
            var signUpModel = new SignUpViewModel
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
                _logger.LogError($"{nameof(AuthenticationService)}: sign up: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (model.Email is null)
            {
                _logger.LogError($"{nameof(AuthenticationService)}: sign up: email was null");

                throw new ArgumentNullException(nameof(model.Email));
            }

            if (model.RawPassword is null)
            {
                _logger.LogError($"{nameof(AuthenticationService)}: sign up: raw password was null");

                throw new ArgumentNullException(nameof(model.RawPassword));
            }

            using var client = _httpClientFactory.CreateClient("Sign Up client");
            client.PrepareJsonRequest($"{MicroservicesUrls.IdentityServer}Authentication/");

            var mappedRequestModel = _mapper.Map<SignUpViewModel, UserSignUpRequest>(model);

            var responseMessage = await client.PostAsJsonAsync("SignUp", mappedRequestModel);

            if (!responseMessage.IsSuccessStatusCode)
            {
                _logger.LogError($"Request from {nameof(AuthenticationService)}, Sign Up: to Identity server returned a server error {responseMessage.StatusCode}");

                if (responseMessage.StatusCode == HttpStatusCode.UnprocessableEntity)
                    throw new AuthenticationException($"Unprocessable Entity: {nameof(SignUpViewModel)}");
            }

            var signUpResponseContent = await responseMessage.Content.ReadFromJsonAsync<UserSignUpResponse>();

            return signUpResponseContent;
        }

        #endregion

        #region Forgot password

        public async Task<UserForgotPasswordResponse> ForgotPasswordAsync(string email)
        {
            var forgotPasswordModel = new ForgotPasswordViewModel
                                      {
                                          Email = email
                                      };

            return await ForgotPasswordAsync(forgotPasswordModel);
        }

        public async Task<UserForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            if (model is null)
            {
                _logger.LogError($"{nameof(AuthenticationService)}: forgot password: model was null");

                throw new ArgumentNullException(nameof(model));
            }

            if (model.Email is null)
            {
                _logger.LogError($"{nameof(AuthenticationService)}: forgot password: email was null");

                throw new ArgumentNullException(nameof(model.Email));
            }

            using var client = _httpClientFactory.CreateClient("Forgot password client");
            client.PrepareJsonRequest($"{MicroservicesUrls.IdentityServer}Authentication/");

            var mappedRequestModel = _mapper.Map<ForgotPasswordViewModel, UserForgotPasswordRequest>(model);

            var responseMessage = await client.PostAsJsonAsync("ForgotPassword", mappedRequestModel);

            if (!responseMessage.IsSuccessStatusCode)
            {
                _logger.LogError($"Request from {nameof(AuthenticationService)}, Forgot Password: to Identity server returned a server error {responseMessage.StatusCode} with email = {model.Email}");

                if (responseMessage.StatusCode == HttpStatusCode.UnprocessableEntity)
                    throw new NetworkInformationException((int)responseMessage.StatusCode);

                return new UserForgotPasswordResponse
                       {
                           IsUserExists = false,
                           Email = model.Email,
                           Error = "User not exists",
                           IsConfirmationMailSent = false
                       };
            }

            var forgotPasswordResponseContent =
                await responseMessage.Content.ReadFromJsonAsync<UserForgotPasswordResponse>();

            return forgotPasswordResponseContent;
        }

        #endregion
    }
}