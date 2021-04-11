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

            if (!authenticateResult.Succeeded)
            {
                await context.ForbidAsync(JwtBearerDefaults.AuthenticationScheme);

                return;
            }

            if (authenticateResult.Principal is null)
            {
                await context.ForbidAsync(JwtBearerDefaults.AuthenticationScheme);

                return;
            }

            var isRequestByEmployee = authenticateResult.Principal.IsInRole(ApplicationRoles.Administrator)
                                      || authenticateResult.Principal.IsInRole(ApplicationRoles.Manager);

            if (isRequestByEmployee)
            {
                await next(context);

                return;
            }

            var isRequestWithToken = context.Request.Headers.TryGetValue("HARDWARE-ACCESS-TOKEN", out var result);

            if (!isRequestWithToken || result.Count == 0)
            {
                await context.ForbidAsync(JwtBearerDefaults.AuthenticationScheme);

                return;
            }

            using var client = new HttpClient();

            // JWT authorization is not need
            //var authorizationToken = context.Request.Headers["Authorization"][0]
            //                         ?? throw new
            //                             Exception($"{nameof(AccessTokenValidatorMiddleware)}: Authorization header not found");

            //authorizationToken = authorizationToken.Replace($"{JwtBearerDefaults.AuthenticationScheme} ", String.Empty);

            //client.PrepareRequestWithAuthentication(JwtBearerDefaults.AuthenticationScheme,
            //                                        authorizationToken,
            //                                        $"{MicroservicesUrls.IdentityServer}Tokens/Check/");

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