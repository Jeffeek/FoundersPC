using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FoundersPC.API.Controllers
{
    public static class LogHelper
    {
        public static void LogForModelUpdated<T>(this ILogger<T> logger, HttpContext httpContext, int modelId)
        {
            logger.LogInformation($"{nameof(T)}: request for updating model with id = {modelId} by {httpContext.User.Identity?.Name ?? "Unknown"}");
        }

        public static void LogForModelInserted<T>(this ILogger<T> logger, HttpContext httpContext)
        {
            logger.LogInformation($"{nameof(T)}: request for inserting by {httpContext.User.Identity?.Name ?? "Unknown"}");
        }

        public static void LogForModelDeleted<T>(this ILogger<T> logger, HttpContext httpContext, int modelId)
        {
            logger.LogInformation($"{nameof(T)}: request for deleting model with id = {modelId} by {httpContext.User.Identity?.Name ?? "Unknown"}");
        }

        public static void LogForModelRead<T>(this ILogger<T> logger, HttpContext httpContext, int modelId)
        {
            logger.LogInformation($"{nameof(T)}: request for reading (one) by {httpContext.User.Identity?.Name ?? "Unknown"}");
        }

        public static void LogForModelsRead<T>(this ILogger<T> logger, HttpContext httpContext)
        {
            logger.LogInformation($"{nameof(T)}: request for reading (all) by {httpContext.User.Identity?.Name ?? "Unknown"}");
        }
    }
}
