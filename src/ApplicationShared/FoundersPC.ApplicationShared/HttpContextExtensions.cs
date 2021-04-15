#region Using namespaces

using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class HttpContextExtensions
    {
        /// <exception cref="T:System.Net.Sockets.SocketException">The address family is <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> and the address is bad.</exception>
        public static string GetIpAddress(this HttpContext httpContext) => httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
        public static bool IsInRole(this ClaimsPrincipal claims, params string[] roles) => roles.Any(claims.IsInRole);

        public static (string Email, string Role) ParseJwtUserTokenCredentials(this HttpContext httpContext) =>
            (httpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
             httpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType));
    }
}