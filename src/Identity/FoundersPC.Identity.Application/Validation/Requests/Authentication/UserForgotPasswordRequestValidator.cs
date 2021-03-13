#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Authentication;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Authentication
{
    public class UserForgotPasswordRequestValidator : AbstractValidator<UserForgotPasswordRequest>
    {
        public UserForgotPasswordRequestValidator()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}