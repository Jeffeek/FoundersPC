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
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Domain.Common.Authentication;
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

        /// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">When model is unprocessable.</exception>
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
        /// <exception cref="T:System.ArgumentNullException">model is <see langword="null"/></exception>
        public Task<UserLoginResponse> SignInAsync(string emailOrLogin, string rawPassword)
        {
            var signInModel = new SignInViewModel
                              {
                                  LoginOrEmail = emailOrLogin,
                                  RawPassword = rawPassword
                              };

            return SignInAsync(signInModel);
        }

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
        /// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">When model is unprocessable.</exception>
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

            var client = _httpClientFactory.CreateClient("Sign In client");
            client.PrepareJsonRequest($"{MicroservicesUrls.IdentityServer}{IdentityServerRoutes.Authentication.Endpoint}/");

            var mappedRequestModel = _mapper.Map<SignInViewModel, UserSignInRequest>(model);

            var responseMessage = await client.PostAsJsonAsync($"{IdentityServerRoutes.Authentication.SignIn}", mappedRequestModel);

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

        /// <exception cref="T:System.Security.Authentication.AuthenticationException">When model is unprocessable.</exception>
        /// <exception cref="T:System.ArgumentNullException">model is <see langword="null"/></exception>
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
        public Task<UserSignUpResponse> SignUpAsync(string email, string rawPassword)
        {
            var signUpModel = new SignUpViewModel
                              {
                                  Email = email,
                                  RawPassword = rawPassword
                              };

            return SignUpAsync(signUpModel);
        }

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
        /// <exception cref="T:System.Security.Authentication.AuthenticationException">When model is unprocessable.</exception>
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

            var client = _httpClientFactory.CreateClient("Sign Up client");
            client.PrepareJsonRequest($"{MicroservicesUrls.IdentityServer}{IdentityServerRoutes.Authentication.Endpoint}/");

            var mappedRequestModel = _mapper.Map<SignUpViewModel, UserSignUpRequest>(model);

            var responseMessage = await client.PostAsJsonAsync($"{IdentityServerRoutes.Authentication.SignUp}", mappedRequestModel);

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

        /// <exception cref="T:System.ArgumentNullException">model is <see langword="null"/></exception>
        /// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">UnprocessableEntity.</exception>
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
        public Task<UserForgotPasswordResponse> ForgotPasswordAsync(string email)
        {
            var forgotPasswordModel = new ForgotPasswordViewModel
                                      {
                                          Email = email
                                      };

            return ForgotPasswordAsync(forgotPasswordModel);
        }

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
        /// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">UnprocessableEntity.</exception>
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

            var client = _httpClientFactory.CreateClient("Forgot password client");
            client.PrepareJsonRequest($"{MicroservicesUrls.IdentityServer}{IdentityServerRoutes.Authentication.Endpoint}/");

            var mappedRequestModel = _mapper.Map<ForgotPasswordViewModel, UserForgotPasswordRequest>(model);

            var responseMessage = await client.PostAsJsonAsync($"{IdentityServerRoutes.Authentication.ForgotPassword}", mappedRequestModel);

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