﻿#region Using namespaces

using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

#endregion

namespace FoundersPC.SharedKernel.Endpoints;

[Route("api")]
public class BaseRequestResponseAnonymousEndpoint<TRequest, TResponse, TEndpoint> : EndpointBaseAsync.WithRequest<TRequest>.WithResult<TResponse>
    where TRequest : IRequest<TResponse>
{
    private ILogger? _logger = null!;
    private IMediator? _mediator = null!;

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;
    protected ILogger Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TEndpoint>>()!)!;

    public override async Task<TResponse> HandleAsync(TRequest command, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation($"{typeof(TRequest).Name}: {await Task.Factory.StartNew(() => JsonConvert.SerializeObject(command), cancellationToken)}");

        return await Mediator.Send(command, cancellationToken);
    }
}