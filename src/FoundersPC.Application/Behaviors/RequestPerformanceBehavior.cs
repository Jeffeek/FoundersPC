#region Using namespaces

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Settings;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

#endregion

namespace FoundersPC.Application.Behaviors;

public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<TRequest> _logger;
    private readonly PipelineSettings _pipelineSettings;
    private readonly Stopwatch _timer;

    public RequestPerformanceBehavior(ILogger<TRequest> logger,
                                      IOptions<PipelineSettings> pipelineSettings,
                                      ICurrentUserService currentUserService)
    {
        _timer = new();

        _logger = logger;
        _currentUserService = currentUserService;
        _pipelineSettings = pipelineSettings.Value;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= _pipelineSettings.LongRunningRequestWarning)
            return response;

        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId;

        if (requestName == "TokenRequest")
            _logger.LogWarning("FoundersPC Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId}",
                               requestName,
                               elapsedMilliseconds,
                               userId);
        else
            _logger.LogWarning("FoundersPC Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {Request}",
                               requestName,
                               elapsedMilliseconds,
                               userId,
                               JsonConvert.SerializeObject(request));

        return response;
    }
}