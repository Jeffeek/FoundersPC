using FoundersPC.ApplicationShared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FoundersPC.API.Controllers
{
    public static class LogHelper
    {
        public static void LogForModelUpdated<T>(this ILogger<T> logger, HttpContext httpContext, int modelId)
        {
            logger.LogInformation($"{typeof(T).Name}: request for updating model with id = {modelId} by {httpContext.User.Identity?.Name ?? "Unknown"}, IPv4 : {httpContext.GetIpAddress()}");
        }

        public static void LogForModelInserted<T>(this ILogger<T> logger, HttpContext httpContext)
        {
            logger.LogInformation($"{typeof(T).Name}: request for inserting by {httpContext.User.Identity?.Name ?? "Unknown"}, IPv4 : {httpContext.GetIpAddress()}");
        }

        public static void LogForModelDeleted<T>(this ILogger<T> logger, HttpContext httpContext, int modelId)
        {
            logger.LogInformation($"{typeof(T).Name}: request for deleting model with id = {modelId} by {httpContext.User.Identity?.Name ?? "Unknown"}, IPv4 : {httpContext.GetIpAddress()}");
        }

        public static void LogForModelRead<T>(this ILogger<T> logger, HttpContext httpContext, int modelId)
        {
            logger.LogInformation($"{typeof(T).Name}: request for reading with id = {modelId} (one) by {httpContext.User.Identity?.Name ?? "Unknown"}, IPv4 : {httpContext.GetIpAddress()}");
        }

        public static void LogForModelsRead<T>(this ILogger<T> logger, HttpContext httpContext)
        {
            logger.LogInformation($"{typeof(T).Name}: request for reading (all) by {httpContext.User.Identity?.Name ?? "Unknown"}, IPv4 : {httpContext.GetIpAddress()}");
        }
    }
}
