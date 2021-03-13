#region Using namespaces

using FluentValidation;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

#endregion

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