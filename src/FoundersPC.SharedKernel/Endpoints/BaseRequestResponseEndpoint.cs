#region Using namespaces

using Ardalis.ApiEndpoints;
using FoundersPC.SharedKernel.ApplicationConstants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.SharedKernel.Endpoints;

[Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
[Route("api")]
public abstract class BaseRequestResponseEndpoint<TRequest, TResponse, TEndpoint> : BaseAsyncEndpoint.WithRequest<TRequest>.WithResponse<TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private ILogger _logger = null!;
    private IMediator _mediator = null!;

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;
    protected ILogger Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TEndpoint>>()!)!;
}