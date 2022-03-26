using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.UserInformation;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public GetRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;

        //RuleFor(x => x.Login)
        //    .NotNull()
        //    .NotEmpty()
        //    .MustAsync(UserExistsAsync)
        //    .When(x => x.Id == null);

        //When(x => String.IsNullOrEmpty(x.Login),
        //     () =>
        //     {
        //         RuleFor(x => x.Id)
        //             .NotNull()
        //             .GreaterThan(0)
        //             .MustAsync((x, _, ct) => UserExistsAsync(x.Id ?? 0, ct));
        //     });
    }

    private async Task<bool> UserExistsAsync(int id, CancellationToken cancellationToken)
    {
        if (id == 0)
            return false;

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<ApplicationUser>()
                       .AnyAsync(x => x.Id == id, cancellationToken);
    }

    private async Task<bool> UserExistsAsync(string? login, CancellationToken cancellationToken)
    {
        if (login == null)
            return false;

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<ApplicationUser>()
                       .AnyAsync(x => x.Login == login, cancellationToken);
    }
}