#region Using namespaces

using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Token.Models;
using FoundersPC.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Endpoints.Account.Token;

public class TokenEndpoint : BaseRequestResponseEndpoint<TokenRequest, TokenResponse, TokenEndpoint>
{
    private readonly IMediator _mediator;

    public TokenEndpoint(IMediator mediator) => _mediator = mediator;

    [HttpPost("token")]
    //[Consumes("application/x-www-form-urlencoded")]
    public override async Task<ActionResult<TokenResponse>> HandleAsync([FromForm] TokenRequest request, CancellationToken cancellationToken = new()) =>
        await _mediator.Send(request, cancellationToken);
}