using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Domain.Enums;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class RestoreHardware : AbstractValidator<RestoreRequest>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    protected RestoreHardware(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;

        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.HardwareTypeId)
            .Must(x => Enum.GetValues<HardwareType>()
                           .Any(z => (int)z == x))
            .WithMessage("Hardware Type Id is incorrect or not found");

        When(x => Enum.GetValues<HardwareType>()
                      .Any(z => (int)z == x.HardwareTypeId),
             () =>
             {
                 RuleFor(x => x.Id)
                     .MustAsync((req, _, ct) => DeletedHardwareExists(req.HardwareTypeId, req.Id, ct))
                     .WithMessage(x => $"Deleted Hardware with Type {x.HardwareTypeId} and Id {x.Id} not found");
             });
    }

    private async Task<bool> DeletedHardwareExists(int hardwareTypeId, int id, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<Domain.Entities.Hardware.Hardware>()
                       .AnyAsync(x => x.HardwareTypeId == hardwareTypeId && x.Id == id && x.IsDeleted, cancellationToken);
    }
}