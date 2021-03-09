using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Authentication;

namespace FoundersPC.Identity.Application.Validation.Requests.Authentication
{
    public class UserSignUpRequestValidator : AbstractValidator<UserSignUpRequest>
    {
        public UserSignUpRequestValidator()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(model => model.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30);
        }
    }
}
