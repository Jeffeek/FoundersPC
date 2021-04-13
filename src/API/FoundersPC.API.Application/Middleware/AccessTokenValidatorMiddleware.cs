#region Using namespaces

using System.Net.Http;
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

            if (authenticateResult.Succeeded && authenticateResult.Principal is not null)
            {
                var isRequestByEmployee = authenticateResult.Principal.IsInRole(ApplicationRoles.Administrator)
                                          || authenticateResult.Principal.IsInRole(ApplicationRoles.Manager);

                if (isRequestByEmployee)
                    await next(context);
            }
            else
            {
                var isRequestWithToken = context.Request.Headers.TryGetValue("HARDWARE-ACCESS-TOKEN", out var result);

                if (!isRequestWithToken || result.Count == 0)
                {
                    await context.ForbidAsync(JwtBearerDefaults.AuthenticationScheme);

                    return;
                }

                using var client = new HttpClient();

                var response = await client.GetAsync($"{MicroservicesUrls.IdentityServer}Tokens/Check/{result[0]}");

                if (!response.IsSuccessStatusCode)
                {
                    await context.ForbidAsync(JwtBearerDefaults.AuthenticationScheme);

                    return;
                }

                await next(context);
            }
        }
    }
}