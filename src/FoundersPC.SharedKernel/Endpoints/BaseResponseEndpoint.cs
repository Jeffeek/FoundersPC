#region Using namespaces

using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.SharedKernel.Endpoints;

[Authorize]
[Route("api")]
public abstract class BaseResponseEndpoint<TResponse, TEndpoint> : EndpointBaseAsync.WithoutRequest.WithResult<TResponse>
{
    private ILogger _logger = null!;
    private IMediator _mediator = null!;

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;
    protected ILogger Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TEndpoint>>()!)!;
}