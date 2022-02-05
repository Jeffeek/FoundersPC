using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.ForgotPassword;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.Web.Endpoints.Account;

public class ForgotPasswordEndpoint : BaseRequestResponseAnonymousEndpoint<ForgotPasswordRequest, ForgotPasswordResponse, ForgotPasswordEndpoint>
{
    [HttpPost("ForgotPassword")]
    public override async Task<ActionResult<ForgotPasswordResponse>> HandleAsync([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken = new()) =>
        await Mediator.Send(request, cancellationToken);
}