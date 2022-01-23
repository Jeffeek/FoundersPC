using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Application.Features.Pricing.Models;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Pricing;

public class BuyRequestValidator : AbstractValidator<BuyRequest>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public BuyRequestValidator(ICurrentUserService currentUserService,
                               IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;

        RuleFor(x => x.PackageType)
            .IsInEnum();

        RuleFor(x => currentUserService.UserId)
            .MustAsync(UserExistAsync)
            .WithMessage($"Not found user with id {currentUserService.UserId}");
    }

    private async Task<bool> UserExistAsync(int id, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<ApplicationUser>()
                       .AnyAsync(x => x.Id == id, cancellationToken);
    }
}