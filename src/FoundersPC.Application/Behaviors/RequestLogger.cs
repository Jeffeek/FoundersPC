#region Using namespaces

using System.Threading;
using System.Threading.Tasks;
using FoundersPC.SharedKernel.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

#endregion

namespace FoundersPC.Application.Behaviors;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger _logger;

    public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId;

        if (requestName == "TokenRequest")
            _logger.LogDebug("Request: {Name} {@UserId}", requestName, userId);
        else _logger.LogDebug("Request: {Name} {@UserId} {Request}", requestName, userId, JsonConvert.SerializeObject(request));

        return Task.CompletedTask;
    }
}