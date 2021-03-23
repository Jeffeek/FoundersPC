using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Unblocking;

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.User_Unblocking
{
    public class UnblockUserByEmailRequestValidator : AbstractValidator<UnblockUserByEmailRequest>
    {
        public UnblockUserByEmailRequestValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
