#region Using namespaces

using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Application.Features.Token.Models;

public class TokenRequest : IRequest<TokenResponse>
{
    [FromForm(Name = "GrantType")]
    public string GrantType { get; set; }
    [FromForm(Name = "Login")]
    public string Login { get; set; }
    [FromForm(Name = "Password")]
    public string Password { get; set; }
    [FromForm(Name = "RefreshToken")]
    public string RefreshToken { get; set; }
}