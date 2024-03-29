﻿#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Unblocking;

#endregion

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