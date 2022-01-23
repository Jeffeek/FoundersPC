using System.Threading;
using System.Threading.Tasks;
using FoundersPC.SharedKernel.ApplicationConstants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.SharedKernel.Endpoints;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Policy = ApplicationAuthorizationPolicies.EmployeePolicy)]
public abstract class BaseRequestResponseManagementEndpoint<TRequest, TResponse, TEndpoint> : BaseRequestResponseEndpoint<TRequest, TResponse, TEndpoint>
    where TRequest : class, IRequest<TResponse>
{
    public override async Task<ActionResult<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}