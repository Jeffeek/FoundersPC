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

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class UpdateHardwareRequestValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : HardwareInfo
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    protected UpdateHardwareRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;

        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.HardwareTypeId)
            .Must(x => Enum.GetValues<HardwareType>()
                           .Any(z => (int)z == x))
            .WithMessage("Hardware Type Id is incorrect or not found");

        RuleFor(x => x.ProducerId)
            .GreaterThan(0)
            .MustAsync(ProducerExistsAsync);

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        When(x => Enum.GetValues<HardwareType>()
                      .Any(z => (int)z == x.HardwareTypeId),
             () =>
             {
                 //RuleFor(x => x.Title)
                 //    .MustAsync((request, _, ct) => HardwareWithSameTitleNotExistsAsync(request.HardwareTypeId, request.Title, ct))
                 //    .WithMessage(x => $"Hardware with Type {x.HardwareTypeId} and Title {x.Title} already exists");

                 RuleFor(x => x.Id)
                     .MustAsync((req, _, ct) => HardwareExists(req.HardwareTypeId, req.Id, ct))
                     .WithMessage(x => $"Hardware with Type {x.HardwareTypeId} and Id {x.Id} not found");
             });
    }

    private async Task<bool> ProducerExistsAsync(int producerId, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<Producer>()
                       .AnyAsync(x => x.Id == producerId, cancellationToken);
    }

    //private async Task<bool> HardwareWithSameTitleNotExistsAsync(int hardwareTypeId, string title, CancellationToken cancellationToken)
    //{
    //    await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

    //    return !await db.Set<Domain.Entities.Hardware.Hardware>()
    //                    .AnyAsync(x => x.HardwareTypeId == hardwareTypeId && x.BaseMetadata.Title == title, cancellationToken);
    //}

    private async Task<bool> HardwareExists(int hardwareTypeId, int id, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<Domain.Entities.Hardware.Hardware>()
                       .AnyAsync(x => x.HardwareTypeId == hardwareTypeId && x.Id == id, cancellationToken);
    }
}