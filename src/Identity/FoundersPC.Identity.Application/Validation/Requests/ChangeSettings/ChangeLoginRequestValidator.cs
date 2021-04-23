#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.IdentityServer.Request.ChangeSettings;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.ChangeSettings
{
    public class ChangeLoginRequestValidator : AbstractValidator<ChangeLoginRequest>
    {
        public ChangeLoginRequestValidator()
        {
            RuleFor(model => model.NewLogin)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(30);
        }
    }
}