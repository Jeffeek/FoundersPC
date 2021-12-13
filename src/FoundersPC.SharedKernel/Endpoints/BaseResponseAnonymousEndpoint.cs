#region Using namespaces

using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.SharedKernel.Endpoints;

public abstract class BaseResponseAnonymousEndpoint<TResponse, TEndpoint> : BaseAsyncEndpoint.WithoutRequest.WithResponse<TResponse>
{
    private ILogger _logger = null!;
    private IMediator _mediator = null!;

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;

    protected ILogger Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TEndpoint>>()!)!;
}