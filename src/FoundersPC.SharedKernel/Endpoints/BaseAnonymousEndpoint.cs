﻿using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FoundersPC.SharedKernel.Endpoints
{
    public class BaseAnonymousEndpoint<TRequest, TResponse, TEndpoint> : BaseAsyncEndpoint.WithRequest<TRequest>.WithResponse<TResponse>
        where TRequest : IRequest<TResponse>
    {
        private IMediator _mediator = null!;

        private ILogger _logger = null!;

        protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;

        protected ILogger Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TEndpoint>>()!)!;

        public override async Task<ActionResult<TResponse>> HandleAsync(TRequest command, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"{typeof(TRequest).Name}: {await Task.Factory.StartNew(() => JsonConvert.SerializeObject(command), cancellationToken)}");

            return await Mediator.Send(command, cancellationToken);
        }
    }
}