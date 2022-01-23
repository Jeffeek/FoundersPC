using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.SignUp;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public SignUpRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MustAsync(EmailNotExistInSystem)
            .WithMessage(x => $"Email {x.Email} already exists in system");

        RuleFor(x => x.Login)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(30)
            .MustAsync(LoginNotExistInSystem)
            .WithMessage(x => $"Login {x.Login} already exists in system");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(30)
            .Must((req, _) => req.Password == req.RepeatPassword)
            .WithMessage("Password != RepeatPassword");

        RuleFor(x => x.RepeatPassword)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(30)
            .Must((req, _) => req.RepeatPassword == req.Password)
            .WithMessage("Password != RepeatPassword");
    }

    private async Task<bool> EmailNotExistInSystem(string email, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return !await db.Set<ApplicationUser>()
                        .AnyAsync(x => x.Email == email, cancellationToken);
    }

    private async Task<bool> LoginNotExistInSystem(string login, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return !await db.Set<ApplicationUser>()
                        .AnyAsync(x => x.Login == login, cancellationToken);
    }
}