using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;

namespace FoundersPC.Web.Application.Validation.AccountSettings
{
    public class PasswordSettingsViewModelValidator : AbstractValidator<PasswordSettingsViewModel>
    {
        public PasswordSettingsViewModelValidator()
        {
            RuleFor(model => model.NewPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .Equal(model => model.NewPasswordConfirm);

            RuleFor(model => model.NewPasswordConfirm)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .Equal(model => model.NewPassword);

            RuleFor(model => model.OldPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30);
        }
    }
}
