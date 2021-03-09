using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Authentication;

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
