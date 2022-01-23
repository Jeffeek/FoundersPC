#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Application.Services.Identity;

public class UserStore :
    IUserStore<ApplicationUser>,
    IUserPasswordStore<ApplicationUser>,
    IUserLockoutStore<ApplicationUser>,
    IUserRoleStore<ApplicationUser>,
    IUserEmailStore<ApplicationUser>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserStore(IMapper mapper,
                     IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _mapper = mapper;
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (email == null)
            throw new ArgumentNullException(nameof(email));

        if (user.Email == email)
            return Task.CompletedTask;

        user.Email = email;

        return UpdateAsync(user, cancellationToken);
    }

    public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken) =>
        Task.FromResult(true);

    public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    public Task<ApplicationUser> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(email))
            throw new ArgumentException(nameof(email));

        return _dbContext.Users
                         .AsNoTracking()
                         .Include(x => x.ApplicationRole)
                         .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.Email);
    }

    public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return Task.CompletedTask;
    }

    public Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken) =>
        Task.FromResult(false);

    public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken) =>
        Task.FromResult(new DateTimeOffset?(DateTimeOffset.MaxValue));

    public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken) =>
        Task.FromResult(0);

    public Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken) =>
        Task.FromResult(user.PasswordHash);

    public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken) =>
        Task.FromResult(user.PasswordHash != null);

    public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.PasswordHash = passwordHash;

        return UpdateAsync(user, cancellationToken);
    }

    public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (String.IsNullOrWhiteSpace(roleName))
            throw new ArgumentException("roleName");

        var role = await _dbContext.Roles
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(x => x.Name == roleName, cancellationToken);

        if (role == null)
            throw new NotFoundException($"Not found role with Name {roleName}");

        if (user.RoleId == role.Id)
            return;

        user.RoleId = role.Id;
        user.ApplicationRole = role;

        await UpdateAsync(user, cancellationToken);
    }

    public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var result = new List<string>();

        if (user.ApplicationRole == null) return Task.FromResult((IList<string>)result);

        result.Add(user.ApplicationRole.Name);

        return Task.FromResult((IList<string>)result);
    }

    public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        if (String.IsNullOrWhiteSpace(roleName))
            throw new ArgumentException(nameof(roleName));

        return Task.FromResult(user.ApplicationRole.Name == roleName);
    }

    public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (String.IsNullOrWhiteSpace(roleName))
            throw new ArgumentException(nameof(roleName));

        return Task.CompletedTask;
    }

    public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(roleName))
            throw new ArgumentException(nameof(roleName));

        return (await _dbContext.Roles
                                .AsNoTracking()
                                .Include(x => x.Users)
                                .FirstOrDefaultAsync(x => x.Name == roleName, cancellationToken))
               ?.Users.ToList()
               ?? new List<ApplicationUser>();
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _dbContext.BeginTransactionAsync(cancellationToken);
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.CommitTransactionAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _dbContext.BeginTransactionAsync(cancellationToken);
        _dbContext.Users.Update(user);
        await _dbContext.CommitTransactionAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        user.IsBlocked = true;

        return UpdateAsync(user, cancellationToken);
    }

    public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (userId == null)
            throw new ArgumentNullException(nameof(userId));

        return _dbContext.Users
                         .AsNoTracking()
                         .Include(x => x.ApplicationRole)
                         .FirstOrDefaultAsync(x => x.Login == userId, cancellationToken);
    }

    public Task<ApplicationUser> FindByNameAsync(string userName, CancellationToken cancellationToken) =>
        FindByIdAsync(userName, cancellationToken);

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.Login);
    }

    public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.Login);
    }

    public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (user == null) throw new ArgumentNullException(nameof(user));

        user.Login = userName;

        return UpdateAsync(user, cancellationToken);
    }

    public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.Login);
    }

    public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.Login = user.Login.Trim();

        return Task.CompletedTask;
    }
}