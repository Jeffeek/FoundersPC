#region Using namespaces

using System;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Logs.Token_Usages;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.Logs.Token_Usages
{
    public class TokensUsagesInRequestValidator : AbstractValidator<TokensUsagesInRequest>
    {
        public TokensUsagesInRequestValidator()
        {
            RuleFor(x => x.RequestDate)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}