#region Using namespaces

using System;

#endregion

namespace FoundersPC.Application.Features.Token.Models;

public class TokenResponse
{
    public DateTime Expires { get; set; }
    public string Issued { get; set; } = default!;
    public int TokenLifetimeInSec { get; set; }
    public string AccessToken { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Login { get; set; } = default!;
}