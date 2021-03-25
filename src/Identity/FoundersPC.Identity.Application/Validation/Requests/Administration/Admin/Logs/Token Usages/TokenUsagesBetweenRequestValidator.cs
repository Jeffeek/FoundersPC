#region Using namespaces

using System;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Logs.Token_Usages;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.Logs.Token_Usages
{
    public class TokenUsagesBetweenRequestValidator : AbstractValidator<TokensUsagesBetweenRequest>
    {
        public TokenUsagesBetweenRequestValidator()
        {
            RuleFor(x => x.Finish)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.Start)
                .NotNull()
                .LessThan(x => x.Finish);
        }
    }
}