#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Domain.Common.Authentication;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication
{
    public interface IAuthenticationWebService
    {
        Task<UserLoginResponse> SignInAsync(string emailOrLogin, string rawPassword);

        Task<UserSignUpResponse> SignUpAsync(string email, string rawPassword);

        Task<UserLoginResponse> SignInAsync(SignInViewModel model);

        Task<UserSignUpResponse> SignUpAsync(SignUpViewModel model);

        Task<UserForgotPasswordResponse> ForgotPasswordAsync(string email);

        Task<UserForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model);
    }
}