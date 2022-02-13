using System.Threading;
using System.Threading.Tasks;
using FoundersPC.SharedKernel.ApplicationConstants;
using FoundersPC.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.Web.Endpoints.Base;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize(Policy = ApplicationAuthorizationPolicies.EmployeePolicy)]
public abstract class BaseRequestResponseManagementEndpoint<TRequest, TResponse, TEndpoint> : BaseRequestResponseEndpoint<TRequest, TResponse, TEndpoint>
    where TRequest : class, IRequest<TResponse>
{
    public override async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = new()) =>
        await Mediator.Send(request, cancellationToken);
}