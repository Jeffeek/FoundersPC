using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class DeleteRequestValidator : AbstractValidator<Domain.Entities.Hardware.Producer>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public DeleteRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .MustAsync(NotDeletedProducer)
            .WithMessage(x => $"Producer with Id {x.Id} already deleted or not exist");
    }

    private async Task<bool> NotDeletedProducer(int id, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<Domain.Entities.Hardware.Producer>()
                       .AnyAsync(x => x.Id == id && !x.IsDeleted,
                                 cancellationToken);
    }
}