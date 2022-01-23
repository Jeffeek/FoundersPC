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
using FoundersPC.Domain.Enums;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Pricing;

public class BuyHandler : IRequestHandler<BuyRequest, AccessTokenInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;
    private readonly TokenEncryptorService _tokenEncryptorService;
    private readonly ICurrentUserService _currentUserService;

    public BuyHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper,
                      TokenEncryptorService tokenEncryptorService,
                      ICurrentUserService currentUserService)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _tokenEncryptorService = tokenEncryptorService;
        _currentUserService = currentUserService;
    }

    public async Task<AccessTokenInfo> Handle(BuyRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var (start, finish) = GetTokenDates(request.PackageType);
        var newToken = new AccessToken
                       {
                           Type = request.PackageType,
                           UserId = currentUserId,
                           StartEvaluationDate = start,
                           ExpirationDate = finish,
                           Token = TokenEncryptorService.CreateToken()
                       };

        await db.Set<AccessToken>()
                .AddAsync(newToken, cancellationToken);

        await db.CommitTransactionAsync(cancellationToken);

        return await db.Set<AccessToken>()
                       .AsNoTracking()
                       .Where(x => x.Id == newToken.Id)
                       .ProjectTo<AccessTokenInfo>(_mapper.ConfigurationProvider)
                       .FirstAsync(cancellationToken);
    }

    private static (DateTime Start, DateTime Finish) GetTokenDates(TokenPackageType type) =>
        type switch
        {
            TokenPackageType.Personal  => (DateTime.Now, DateTime.Now.AddDays(7)),
            TokenPackageType.ProPlan   => (DateTime.Now, DateTime.Now.AddMonths(1)),
            TokenPackageType.Unlimited => (DateTime.Now, new(9999, 1, 1, 1, 1, 1)),
            _                          => throw new ArgumentOutOfRangeException(nameof(type))
        };
}