#region Using namespaces

using FluentValidation;
using FoundersPC.SharedKernel;

#endregion

namespace FoundersPC.Application.Features.Token.Models;

public class TokenRequestValidation : AbstractValidator<TokenRequest>
{
    public TokenRequestValidation()
    {
        RuleFor(x => x.GrantType)
            .Must(x => x is GrantTypes.Password or GrantTypes.RefreshToken)
            .WithMessage("Unsupported grant type");

        RuleFor(x => x.RefreshToken)
            .NotNull()
            .NotEmpty()
            .When(x => x.GrantType == GrantTypes.RefreshToken);
    }
}