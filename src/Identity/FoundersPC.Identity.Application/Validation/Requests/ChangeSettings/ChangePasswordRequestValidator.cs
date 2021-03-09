#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.ChangeSettings
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(model => model.OldPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .NotEqual(model => model.NewPassword);

            RuleFor(model => model.NewPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .NotEqual(model => model.OldPassword);
        }
    }
}