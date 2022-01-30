using System;
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FoundersPC.Application.Features.UserInformation;

public class UnblockHandler : IRequestHandler<UnblockRequest, Unit>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IEmailService _emailService;
    private readonly ILogger<UnblockHandler> _logger;

    public UnblockHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory, IEmailService emailService, ILogger<UnblockHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Unit> Handle(UnblockRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var user = await db.Set<ApplicationUser>()
                           .Include(x => x.Tokens)
                           .FirstOrDefaultAsync(x => x.Id == request.Id,
                                                cancellationToken)
                   ?? throw new NotFoundException("User", request.Id);

        user.IsBlocked = false;

        await db.CommitTransactionAsync(cancellationToken);

        try
        {
            await _emailService.SendUnblockNotificationAsync(user.Email, request.Reason);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when sending email");
        }

        return Unit.Value;
    }
}