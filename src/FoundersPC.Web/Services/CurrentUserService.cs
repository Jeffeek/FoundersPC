#region Using namespaces

using System;
using System.Security.Claims;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Http;

#endregion

namespace FoundersPC.Web.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");

        if (userId != null && Int32.TryParse(userId, out var result))
            UserId = result;
    }

    public int UserId { get; }
}