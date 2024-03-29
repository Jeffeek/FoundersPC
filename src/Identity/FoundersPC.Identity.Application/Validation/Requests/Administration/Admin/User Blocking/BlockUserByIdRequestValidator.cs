﻿#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Blocking;

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