using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;

namespace FoundersPC.Web.Application.Validation.AccountSettings
{
    public class SecuritySettingsViewModelValidator : AbstractValidator<SecuritySettingsViewModel>
    {
        public SecuritySettingsViewModelValidator()
        {
            RuleFor(model => model.NewLogin)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(30);
        }
    }
}
