#region Using namespaces

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class HttpContextExtensions
    {
        public static string GetIpAddress(this HttpContext httpContext) => httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        public static (string Email, string Role) ParseJwtUserTokenCredentials(this HttpContext httpContext) =>
            (httpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
             httpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType));
    }
}