#region Using namespaces

using System;
using System.Linq;
using System.Security.Claims;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Web.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private int? _userId;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor,
                              IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        Login = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

        if (userId != null && Int32.TryParse(userId, out var result))
            _userId = result;
    }

    public int UserId
    {
        get
        {
            if (_userId.HasValue && _userId != 0)
                return _userId.Value;

            using var db = _dbContextFactory.CreateDbContext();

            var user = db.Set<ApplicationUser>()
                         .AsNoTracking()
                         .Include(x => x.ApplicationRole)
                         .First(x => x.Login == Login);

            _userId = user.Id;
            Role = user.ApplicationRole.Name;

            return _userId.Value;
        }
    }

    public string Login { get; }

    public string Role { get; private set; }
}