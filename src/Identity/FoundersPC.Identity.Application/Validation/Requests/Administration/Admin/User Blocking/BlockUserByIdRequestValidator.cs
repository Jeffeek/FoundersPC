#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Blocking;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.User_Blocking
{
    public class BlockUserByIdRequestValidator : AbstractValidator<BlockUserByIdRequest>
    {
        public BlockUserByIdRequestValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}