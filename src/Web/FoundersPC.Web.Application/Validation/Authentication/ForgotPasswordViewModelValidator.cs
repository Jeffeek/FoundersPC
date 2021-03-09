using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

namespace FoundersPC.Web.Application.Validation.Authentication
{
    public class ForgotPasswordViewModelValidator : AbstractValidator<ForgotPasswordViewModel>
    {
        public ForgotPasswordViewModelValidator()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
