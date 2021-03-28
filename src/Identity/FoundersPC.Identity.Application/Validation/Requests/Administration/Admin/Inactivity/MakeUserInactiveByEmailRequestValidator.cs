#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Inactivity;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.Inactivity
{
    public class MakeUserInactiveByEmailRequestValidator : AbstractValidator<MakeUserInactiveByEmailRequest>
    {
        public MakeUserInactiveByEmailRequestValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}