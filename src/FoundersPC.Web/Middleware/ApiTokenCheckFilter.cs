﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Identity.Tokens;
using FoundersPC.Domain.Enums;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Web.Middleware;

public class ApiTokenCheckFilter : IAsyncActionFilter
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly ICurrentUserService _currentUserService;

    public ApiTokenCheckFilter(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                               ICurrentUserService currentUserService)
    {
        _dbContextFactory = dbContextFactory;
        _currentUserService = currentUserService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        if (!context.HttpContext.Request.Headers.TryGetValue("api-key", out var token) || token.Count == 0)
            throw new AccessTokenException(AccessTokenExceptionType.NotFoundKey);

        var tokenValue = token[0];

        if (tokenValue.Length != 64)
            throw new AccessTokenException(AccessTokenExceptionType.InvalidAccessToken);

        var tokenEntity = await db.Set<AccessToken>()
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(x => x.Token == tokenValue);

        if (tokenEntity == null)
            throw new AccessTokenException(AccessTokenExceptionType.NotFoundAccessToken);

        if (!await CheckTokenExpiringAsync(tokenEntity))
            throw new AccessTokenException(AccessTokenExceptionType.Expired);

        if (!await CheckTokenBlockingAsync(tokenEntity))
            throw new AccessTokenException(AccessTokenExceptionType.Blocked);

        if (!await CheckTokenTooManyRequestsAsync(db, tokenEntity))
            throw new AccessTokenException(AccessTokenExceptionType.TooManyRequests);

        await AddAccessTokenHistoryAsync(db, tokenEntity);
        await next();
    }

    private static async Task<bool> CheckTokenTooManyRequestsAsync(ApplicationDbContext dbContext, AccessToken accessToken)
    {
        var lastTokenRequest = await dbContext.Set<AccessTokenHistory>()
                                              .AsNoTracking()
                                              .Where(x => x.AccessTokenId == accessToken.Id)
                                              .OrderByDescending(x => x.RequestDate)
                                              .FirstOrDefaultAsync();

        if (lastTokenRequest == null)
            return true;

        return accessToken.Type switch
               {
                   TokenPackageType.Personal  => (DateTime.Now - lastTokenRequest.RequestDate).Minutes >= 1,
                   TokenPackageType.ProPlan   => (DateTime.Now - lastTokenRequest.RequestDate).Seconds >= 30,
                   TokenPackageType.Unlimited => (DateTime.Now - lastTokenRequest.RequestDate).Seconds >= 10,
                   _ => throw new ArgumentOutOfRangeException(nameof(accessToken.Type))
               };
    }

    private static Task<bool> CheckTokenBlockingAsync(AccessToken accessToken) =>
        Task.FromResult(!accessToken.IsBlocked);

    private static Task<bool> CheckTokenExpiringAsync(AccessToken accessToken) =>
        Task.FromResult(accessToken.ExpirationDate > DateTime.Now);

    private async Task AddAccessTokenHistoryAsync(ApplicationDbContext dbContext, AccessToken accessToken)
    {
        await dbContext.BeginTransactionAsync();

        var history = new AccessTokenHistory
                      {
                          AccessTokenId = accessToken.Id,
                          RequestDate = DateTime.Now,
                          RequestUserId = _currentUserService.UserId
                      };

        await dbContext.Set<AccessTokenHistory>()
                       .AddAsync(history);

        await dbContext.CommitTransactionAsync();
    }
}