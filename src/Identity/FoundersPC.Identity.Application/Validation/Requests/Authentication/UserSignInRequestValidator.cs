#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Authentication;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Authentication
{
    public class UserSignInRequestValidator : AbstractValidator<UserSignInRequest>
    {
        public UserSignInRequestValidator()
        {
            RuleFor(model => model.LoginOrEmail)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(30)
                .Unless(model => !model.LoginOrEmail.Contains('@'));

            RuleFor(model => model.LoginOrEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .When(model => model.LoginOrEmail.Contains('@'));
        }
    }
}