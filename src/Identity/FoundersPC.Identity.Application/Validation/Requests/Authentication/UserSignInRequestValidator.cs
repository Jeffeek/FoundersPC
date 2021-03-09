using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Authentication;

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
