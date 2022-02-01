using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public CreateRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;

        RuleFor(x => x.Id)
            .Equal(0);

        RuleFor(x => x.FullName)
            .NotNull()
            .NotEmpty()
            .MustAsync(ProducerWithSameNameNotExists);
    }

    private async Task<bool> ProducerWithSameNameNotExists(string fullName, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return !await db.Set<Domain.Entities.Hardware.Producer>()
                        .AnyAsync(x => x.FullName == fullName, cancellationToken);
    }
}