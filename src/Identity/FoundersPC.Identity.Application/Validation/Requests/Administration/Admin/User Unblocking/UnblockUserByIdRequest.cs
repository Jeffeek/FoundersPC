#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Unblocking;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.User_Unblocking
{
    public class UnblockUserByIdRequestValidator : AbstractValidator<UnblockUserByIdRequest>
    {
        public UnblockUserByIdRequestValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}