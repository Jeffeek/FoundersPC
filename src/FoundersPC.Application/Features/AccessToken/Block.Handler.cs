using System;
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.AccessToken;

public class BlockHandler : IRequestHandler<BlockRequest, Unit>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IEmailService _emailService;

    public BlockHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory, IEmailService emailService)
    {
        _dbContextFactory = dbContextFactory;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(BlockRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var user = await db.Set<Domain.Entities.Identity.Tokens.AccessToken>()
                           .Include(x => x.ApplicationUser)
                           .FirstOrDefaultAsync(x => x.Id == request.Id,
                                                cancellationToken)
                   ?? throw new NotFoundException("AccessToken", request.Id);

        user.IsBlocked = true;

        await db.CommitTransactionAsync(cancellationToken);

        try
        {
            await _emailService.SendAccessTokenBlockNotificationAsync(user.ApplicationUser.Email, user.Token);
        }
        catch (Exception e) { }

        return Unit.Value;
    }
}