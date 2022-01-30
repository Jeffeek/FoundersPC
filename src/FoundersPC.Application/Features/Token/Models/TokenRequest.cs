#region Using namespaces

using MediatR;

#endregion

namespace FoundersPC.Application.Features.Token.Models;

public class TokenRequest : IRequest<TokenResponse>
{
    public string GrantType { get; set; } = default!;
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? RefreshToken { get; set; }
}