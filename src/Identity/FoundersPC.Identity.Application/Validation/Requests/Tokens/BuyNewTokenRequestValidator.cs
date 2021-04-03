#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Tokens;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Tokens
{
    public class BuyNewTokenRequestValidator : AbstractValidator<BuyNewTokenRequest>
    {
        public BuyNewTokenRequestValidator()
        {
            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.UserEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.TokenType)
                .IsInEnum();
        }
    }
}