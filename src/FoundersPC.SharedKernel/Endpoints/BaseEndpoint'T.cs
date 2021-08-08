using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FoundersPC.SharedKernel.Endpoints
{
    [Authorize]
    [Route("api")]
    public abstract class BaseEndpoint<TEndpoint> : BaseAsyncEndpoint.WithoutRequest.WithoutResponse
    {
        private IMediator _mediator = null!;
        private ILogger _logger = null!;

        protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;

        protected ILogger Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TEndpoint>>()!)!;
    }
}