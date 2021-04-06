#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

#endregion

namespace FoundersPC.API.Application.Middleware
{
    public class AccessTokenValidatorMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var authenticateResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                await context.ForbidAsync(JwtBearerDefaults.AuthenticationScheme);

                return;
            }

            var isRequestByEmployee = context.User.IsInRole(ApplicationRoles.Administrator)
                                      || context.User.IsInRole(ApplicationRoles.Manager);

            if (isRequestByEmployee) await next(context);

            var isRequestWithToken = context.Request.Headers.TryGetValue("HARDWARE-ACCESS-TOKEN", out var result);

            if (!isRequestWithToken) await context.ForbidAsync(JwtBearerDefaults.AuthenticationScheme);

            // todo: send request to identity server to check the token
            await next(context);
        }
    }
}