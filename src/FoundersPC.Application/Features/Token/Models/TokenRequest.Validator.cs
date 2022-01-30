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
            .WithMessage($"Unsupported grant type. Supported: {GrantTypes.Password} and {GrantTypes.RefreshToken}");

        When(x => x.GrantType is GrantTypes.Password,
             () =>
             {
                 RuleFor(x => x.Login)
                     .NotNull()
                     .NotEmpty();

                 RuleFor(x => x.Password)
                     .NotNull()
                     .NotEmpty()
                     .MinimumLength(6)
                     .MaximumLength(30);

                 RuleFor(x => x.RefreshToken)
                     .Null();
             });

        When(x => x.GrantType is GrantTypes.RefreshToken,
             () =>
             {
                 RuleFor(x => x.Login)
                     .Null();

                 RuleFor(x => x.Password)
                     .Null();

                 RuleFor(x => x.RefreshToken)
                     .NotNull()
                     .NotEmpty();
             });
    }
}