using System;
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FoundersPC.Application.Features.UserInformation;

public class BlockHandler : IRequestHandler<BlockRequest, Unit>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IEmailService _emailService;
    private readonly ILogger<BlockHandler> _logger;

    public BlockHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory, IEmailService emailService, ILogger<BlockHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Unit> Handle(BlockRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var user = await db.Set<ApplicationUser>()
                           .Include(x => x.Tokens)
                           .FirstOrDefaultAsync(x => x.Id == request.Id,
                                                cancellationToken)
                   ?? throw new NotFoundException("User", request.Id);

        user.IsBlocked = true;
        user.Tokens.ForEach(x => x.IsBlocked = true);

        await db.CommitTransactionAsync(cancellationToken);

        try
        {
            await _emailService.SendBlockNotificationAsync(user.Email, request.Reason);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when sending email");
        }

        return Unit.Value;
    }
}