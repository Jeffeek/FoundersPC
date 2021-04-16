#region Using namespaces

using FoundersPC.ApplicationShared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers
{
    internal static class LogRequestExtensions
    {
        public static void LogForModelUpdate<T>(this ILogger<T> logger, HttpContext httpContext, int modelId)
        {
            logger.LogInformation($"{typeof(T).Name}: request for updating model with id = {modelId} by {httpContext.User.Identity?.Name ?? "Unknown"}, IPv4 : {httpContext.GetIpAddress()}");
        }

        public static void LogForModelInsert<T>(this ILogger<T> logger, HttpContext httpContext)
        {
            logger.LogInformation($"{typeof(T).Name}: request for inserting by {httpContext.User.Identity?.Name ?? "Unknown"}, IPv4 : {httpContext.GetIpAddress()}");
        }

        public static void LogForModelDelete<T>(this ILogger<T> logger, HttpContext httpContext, int modelId)
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

        public static void LogForPaginateableModelsRead<T>(this ILogger<T> logger,
                                                           HttpContext httpContext,
                                                           int page,
                                                           int size)
        {
            logger.LogInformation($"{typeof(T).Name}: request for reading page: {page}, size: {size} by {httpContext.User.Identity?.Name ?? "Unknown"}, IPv4 : {httpContext.GetIpAddress()}");
        }
    }
}