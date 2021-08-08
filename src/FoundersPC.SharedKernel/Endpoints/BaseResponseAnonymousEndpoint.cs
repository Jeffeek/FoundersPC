using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FoundersPC.SharedKernel.Endpoints
{
    public abstract class BaseResponseAnonymousEndpoint<TResponse, TEndpoint> : BaseAsyncEndpoint.WithoutRequest.WithResponse<TResponse>
    {
        private IMediator _mediator = null!;
        private ILogger _logger = null!;

        protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;

        protected ILogger Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TEndpoint>>()!)!;
    }
}