#region Using namespaces

using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Token.Models;
using FoundersPC.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Endpoints.Account.Token;

public class TokenEndpoint : BaseRequestResponseAnonymousEndpoint<TokenRequest, TokenResponse, TokenEndpoint>
{
    [HttpPost("Token")]
    [Consumes("application/x-www-form-urlencoded")]
    public override async Task<ActionResult<TokenResponse>> HandleAsync([FromForm] TokenRequest request, CancellationToken cancellationToken = new()) =>
        await Mediator.Send(request, cancellationToken);
}