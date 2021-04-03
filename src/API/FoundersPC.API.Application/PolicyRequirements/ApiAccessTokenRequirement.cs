#region Using namespaces

using Microsoft.AspNetCore.Authorization;

#endregion

namespace FoundersPC.API.Application.PolicyRequirements
{
    public class ApiAccessTokenRequirement : IAuthorizationRequirement
    {
        public ApiAccessTokenRequirement(string token) => Token = token;

        public string Token { get; set; }
    }
}