#region Using namespaces

using System;
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Application.Services.Identity;

public class RoleStore : IRoleStore<ApplicationRole>
{
    private readonly ApplicationDbContext _dbContext;

    public RoleStore(IDbContextFactory<ApplicationDbContext> dbContextFactory) => _dbContext = dbContextFactory.CreateDbContext();

    public Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(IdentityResult.Success);
    }

    public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        return Task.FromResult(role.Name);
    }

    public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken) => GetRoleIdAsync(role, cancellationToken);

    public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken) =>
        _dbContext.Roles
                  .AsNoTracking()
                  .FirstOrDefaultAsync(x => x.Name == roleId, cancellationToken);

    public Task<ApplicationRole> FindByNameAsync(string roleName, CancellationToken cancellationToken) => FindByIdAsync(roleName, cancellationToken);

    public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(IdentityResult.Success);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public Task<ApplicationRole> FindByIdAsync(int roleId, CancellationToken cancellationToken) =>
        _dbContext.Roles
                  .AsNoTracking()
                  .FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
}