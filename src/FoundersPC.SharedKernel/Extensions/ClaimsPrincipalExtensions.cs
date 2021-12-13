#region Using namespaces

using System.Security.Claims;

#endregion

namespace FoundersPC.SharedKernel.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAuthenticated(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.Identity?.IsAuthenticated ?? false;
}