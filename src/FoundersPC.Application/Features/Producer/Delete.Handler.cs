using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class DeleteHandler : IRequestHandler<DeleteRequest, Unit>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public DeleteHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var producer = await db.Set<Domain.Entities.Hardware.Producer>()
                               .Where(x => x.Id == request.Id)
                               .FirstOrDefaultAsync(cancellationToken);

        if (producer == null)
            throw new NotFoundException($"Not found Producer with Id {request.Id}");

        db.Set<Domain.Entities.Hardware.Producer>()
          .Remove(producer);

        await db.CommitTransactionAsync(cancellationToken);

        return Unit.Value;
    }
}