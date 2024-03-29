﻿#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Blocking;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.User_Blocking
{
    public class BlockUserByEmailRequestValidator : AbstractValidator<BlockUserByEmailRequest>
    {
        public BlockUserByEmailRequestValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}