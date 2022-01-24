using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Token.Models;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.SignUp;

public class SignUpHandler : IRequestHandler<SignUpRequest, TokenResponse>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMediator _mediator;

    public SignUpHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IPasswordHasher<ApplicationUser> passwordHasher,
                         IDateTimeService dateTimeService,
                         IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _passwordHasher = passwordHasher;
        _dateTimeService = dateTimeService;
        _mediator = mediator;
    }

    public async Task<TokenResponse> Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var userRole = await db.Set<ApplicationRole>()
                               .AsNoTracking()
                               .FirstAsync(x => x.Name == "DefaultUser", cancellationToken);

        await db.BeginTransactionAsync(cancellationToken);

        var user = new ApplicationUser
                   {
                       Email = request.Email,
                       Login = request.Login,
                       RegistrationDate = _dateTimeService.Now,
                       RoleId = userRole.Id,
                       IsBlocked = false
                   };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        await db.Set<ApplicationUser>()
                .AddAsync(user, cancellationToken);

        await db.CommitTransactionAsync(cancellationToken);

        return await _mediator.Send(new TokenRequest
                                    {
                                        GrantType = GrantTypes.Password,
                                        Login = request.Login,
                                        Password = request.Password
                                    },
                                    cancellationToken);
    }
}