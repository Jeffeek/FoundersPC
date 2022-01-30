using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.Pricing.Models;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.Application.Services;
using FoundersPC.Domain.Entities.Identity.Tokens;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Domain.Enums;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Interfaces;
using FoundersPC.SharedKernel.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FoundersPC.Application.Features.Pricing;

public class BuyHandler : IRequestHandler<BuyRequest, AccessTokenInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmailService _emailService;
    private readonly ILogger<BuyHandler> _logger;
    private readonly AccessTokenPlans _accessTokenPlans;

    public BuyHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper,
                      ICurrentUserService currentUserService,
                      IOptions<AccessTokenPlans> accessTokenPlans,
                      IEmailService emailService,
                      ILogger<BuyHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _emailService = emailService;
        _logger = logger;
        _accessTokenPlans = accessTokenPlans.Value;
    }

    public async Task<AccessTokenInfo> Handle(BuyRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var (start, finish) = GetTokenDates(request.PackageType);
        var newToken = new Domain.Entities.Identity.Tokens.AccessToken
                       {
                           Type = request.PackageType,
                           UserId = currentUserId,
                           StartEvaluationDate = start,
                           ExpirationDate = finish,
                           Token = AccessTokenFactory.CreateToken()
                       };

        await db.Set<Domain.Entities.Identity.Tokens.AccessToken>()
                .AddAsync(newToken, cancellationToken);

        await db.CommitTransactionAsync(cancellationToken);

        var currentUserEmail = await db.Set<ApplicationUser>()
                                       .Where(x => x.Id == currentUserId)
                                       .Select(x => x.Email)
                                       .FirstAsync(cancellationToken);

        try
        {
            await _emailService.SendAPIAccessTokenAsync(currentUserEmail, newToken.Token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when sending email");
        }

        return await db.Set<Domain.Entities.Identity.Tokens.AccessToken>()
                       .AsNoTracking()
                       .Where(x => x.Id == newToken.Id)
                       .ProjectTo<AccessTokenInfo>(_mapper.ConfigurationProvider)
                       .FirstAsync(cancellationToken);
    }

    private (DateTime Start, DateTime Finish) GetTokenDates(TokenPackageType type) =>
        type switch
        {
            TokenPackageType.Personal  => (DateTime.Now, DateTime.Now.AddSeconds(_accessTokenPlans.Personal.AddSeconds)),
            TokenPackageType.ProPlan   => (DateTime.Now, DateTime.Now.AddSeconds(_accessTokenPlans.ProPlan.AddSeconds)),
            TokenPackageType.Unlimited => (DateTime.Now, DateTime.Now.AddSeconds(_accessTokenPlans.Unlimited.AddSeconds)),
            _                          => throw new ArgumentOutOfRangeException(nameof(type))
        };
}