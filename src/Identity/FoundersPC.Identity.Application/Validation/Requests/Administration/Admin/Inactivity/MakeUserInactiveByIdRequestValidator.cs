#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Inactivity;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.Inactivity
{
    public class MakeUserInactiveByIdRequestValidator : AbstractValidator<MakeUserInactiveByIdRequest>
    {
        public MakeUserInactiveByIdRequestValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}