#region Using namespaces

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

#endregion

namespace FoundersPC.Web.Application.Middleware
{
    public class CookieCheckMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var authenticateResult = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                await next(context);

                return;
            }

            context.Request.Cookies.TryGetValue("token", out var jwtCookieAuthentication);
            context.Request.Cookies.TryGetValue("user_cred", out var defaultCookieAuthentication);

            if (jwtCookieAuthentication is null || defaultCookieAuthentication is null)
            {
                context.Response.Cookies.Delete("cookie");
                context.Response.Cookies.Delete("user_cred");
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Cookies.Delete("token");

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Redirect("/Error/401");

                return;
            }

            await next(context);
        }
    }
}