using System;
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Services;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FoundersPC.Application.Features.ForgotPassword;

public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IEmailService _emailService;
    private readonly PasswordEncryptorService _passwordEncryptorService;
    private readonly ILogger<ForgotPasswordHandler> _logger;

    public ForgotPasswordHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                 IEmailService emailService,
                                 PasswordEncryptorService passwordEncryptorService,
                                 ILogger<ForgotPasswordHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _emailService = emailService;
        _passwordEncryptorService = passwordEncryptorService;
        _logger = logger;
    }

    public async Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        bool result;

        var user = await db.Set<ApplicationUser>()
                           .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user == null)
            return new()
                   {
                       Email = request.Email,
                       Message = "Not found user with provided email",
                       IsSuccess = false
                   };

        var newPassword = PasswordEncryptorService.GeneratePassword(10);
        var hashedPassword = _passwordEncryptorService.EncryptPassword(newPassword);

        await db.BeginTransactionAsync(cancellationToken);
        user.PasswordHash = hashedPassword;

        try
        {
            await _emailService.SendNewPasswordAsync(user.Email, newPassword);
            result = true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured when tried to send email");
            result = false;
        }

        if (result)
        {
            await db.CommitTransactionAsync(cancellationToken);

            return new()
                   {
                       Email = request.Email,
                       Message = "New password was sent to your email :)",
                       IsSuccess = true
                   };
        }

        await db.RollbackTransactionAsync(cancellationToken);

        return new()
               {
                   Email = request.Email,
                   Message = "Our bot can't send you a new password. Please wait a minute and try again :)",
                   IsSuccess = false
               };
    }
}