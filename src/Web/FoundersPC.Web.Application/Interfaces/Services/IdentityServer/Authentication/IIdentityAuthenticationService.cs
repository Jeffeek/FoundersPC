using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication
{
    public interface IIdentityAuthenticationService
    {
        Task<UserLoginResponse> SignInAsync(string emailOrLogin, string rawPassword);

        Task<UserRegisterResponse> SignUpAsync(string email, string rawPassword);

        Task<UserLoginResponse> SignInAsync(SignInViewModel model);

        Task<UserRegisterResponse> SignUpAsync(SignUpViewModel model);

        Task<UserForgotPasswordResponse> ForgotPasswordAsync(string email);

        Task<UserForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model);
    }
}
