#region Using namespaces

using FluentValidation;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

#endregion

namespace FoundersPC.Web.Application.Validation.Authentication
{
    public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelValidator()
        {
            RuleFor(model => model.LoginOrEmail)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5);
            RuleFor(model => model.LoginOrEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .When(x => x.LoginOrEmail.Contains('@'));

            RuleFor(model => model.RawPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30);
        }
    }
}