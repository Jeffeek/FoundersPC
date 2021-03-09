using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

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
