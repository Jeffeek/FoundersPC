#region Using namespaces

using Microsoft.AspNetCore.Http;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class HttpContextExtensions
    {
        public static string GetIpAddress(this HttpContext httpContext) => httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }
}