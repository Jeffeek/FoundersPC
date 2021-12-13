#region Using namespaces

using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

#endregion

namespace FoundersPC.Application.Features.Models.Token;

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