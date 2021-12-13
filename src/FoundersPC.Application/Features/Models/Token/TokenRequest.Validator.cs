#region Using namespaces

using FluentValidation;
using FoundersPC.SharedKernel;

#endregion

namespace FoundersPC.Application.Features.Models.Token;

public class TokenRequestValidation : AbstractValidator<TokenRequest>
{
    public TokenRequestValidation()
    {
        RuleFor(x => x.GrantType)
            .Must(x => x == GrantTypes.Password
                       || x == GrantTypes.RefreshToken)
            .WithMessage("Unsupported grant type");
    }
}