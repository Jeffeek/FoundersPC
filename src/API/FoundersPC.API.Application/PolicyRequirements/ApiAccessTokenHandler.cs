#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using Microsoft.AspNetCore.Authorization;

#endregion

namespace FoundersPC.API.Application.PolicyRequirements
{
    // todo: delete or not // tried to check token and not if it's an employee
    public class ApiAccessTokenHandler : AuthorizationHandler<ApiAccessTokenRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiAccessTokenRequirement requirement)
        {
            if (context.User.IsInRole(ApplicationRoles.Administrator) || context.User.IsInRole(ApplicationRoles.Manager))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}