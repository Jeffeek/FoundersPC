#region Using namespaces

using System;
using Newtonsoft.Json;

#endregion

namespace FoundersPC.Application.Features.Models.Token;

public class TokenResponse
{
    public DateTime Expires { get; set; }

    public string Issued { get; set; }

    public int TokenLifetimeInSec { get; set; }

    public string AccessToken { get; set; }

    public string Email { get; set; }

    public string RefreshToken { get; set; }

    public string Role { get; set; }

    public string TokenType { get; set; }

    public string Login { get; set; }
}