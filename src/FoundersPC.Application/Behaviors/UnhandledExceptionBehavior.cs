#region Using namespaces

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

#endregion

namespace FoundersPC.Application.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            if (requestName == "TokenRequest")
                _logger.LogError(ex, "FoundersPC Request: Unhandled Exception for Request {Name}", requestName);
            else
                _logger.LogError(ex,
                                 "FoundersPC Request: Unhandled Exception for Request {Name} {Request}",
                                 requestName,
                                 JsonConvert.SerializeObject(request,
                                                             new JsonSerializerSettings
                                                             {
                                                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                             }));

            throw;
        }
    }
}