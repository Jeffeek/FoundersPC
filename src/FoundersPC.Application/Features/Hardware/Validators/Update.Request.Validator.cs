using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;
using HardwareType = FoundersPC.Domain.Enums.HardwareType;

namespace FoundersPC.Application.Features.Hardware.Validators;

public class UpdateBaseValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : HardwareInfo
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    protected UpdateBaseValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;

        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.ProducerId)
            .NotEqual(0)
            .MustAsync(ProducerExistAsync)
            .WithMessage(x => $"Producer with Id {x.ProducerId} not exist");

        RuleFor(x => x.HardwareTypeId)
            .Must(x => Enum.GetValues<HardwareType>()
                           .Any(z => (int)z == x))
            .WithMessage("Hardware Type Id was outside of valid Id's");

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();
    }

    private async Task<bool> ProducerExistAsync(int id, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<Producer>()
                       .AnyAsync(x => x.Id == id, cancellationToken);
    }
}