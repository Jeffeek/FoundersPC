using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.SignUp;
using FoundersPC.Application.Features.Token.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.Web.Endpoints.Account;

public class SignUpEndpoint : BaseRequestResponseEndpoint<SignUpRequest, TokenResponse, SignUpEndpoint>
{
    [HttpPost("SignUp")]
    public override async Task<ActionResult<TokenResponse>> HandleAsync([FromForm] SignUpRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}