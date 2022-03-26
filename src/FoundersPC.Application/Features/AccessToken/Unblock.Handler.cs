using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.AccessToken;

public class UnblockHandler : IRequestHandler<UnblockRequest, Unit>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UnblockHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Unit> Handle(UnblockRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var user = await db.Set<Domain.Entities.Identity.Tokens.AccessToken>()
                           .FirstOrDefaultAsync(x => x.Id == request.Id,
                                                cancellationToken)
                   ?? throw new NotFoundException("AccessToken", request.Id);

        user.IsBlocked = false;

        await db.CommitTransactionAsync(cancellationToken);

        return Unit.Value;
    }
}