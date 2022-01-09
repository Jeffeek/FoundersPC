#region Using namespaces

using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Domain.Common;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Identity.Tokens;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence.Configurations.Hardware;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

#endregion

namespace FoundersPC.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private IDbContextTransaction _currentTransaction;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                ICurrentUserService currentUserService,
                                IDateTimeService dateTimeService) : base(options)
    {
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public DbSet<Processor> Processors { get; set; }
    public DbSet<VideoCard> VideoCards { get; set; }
    public DbSet<HardDriveDisk> HardDrives { get; set; }
    public DbSet<SolidStateDrive> SolidStateDrives { get; set; }
    public DbSet<Motherboard> Motherboards { get; set; }
    public DbSet<PowerSupply> PowerSupplies { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<RandomAccessMemory> RandomAccessMemory { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<AccessToken> AccessTokens { get; set; }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
            return;

        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            await _currentTransaction?.CommitAsync(cancellationToken)!;
        }
        catch
        {
            RollbackTransaction();

            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public override void Dispose()
    {
        if (_currentTransaction != null)
            RollbackTransaction();

        base.Dispose();
    }

    public override ValueTask DisposeAsync()
    {
        if (_currentTransaction != null)
            RollbackTransaction();

        return base.DisposeAsync();
    }

    #region Overrides of DbContext

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HardwareConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc/>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<FullAuditable>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedById = _currentUserService.UserId;
                    entry.Entity.Created = _dateTimeService.Now;
                    entry.Entity.LastModifiedById = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTimeService.Now;

                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedById = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTimeService.Now;

                    break;

                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedById = _currentUserService.UserId;
                    entry.Entity.Deleted = _dateTimeService.Now;
                    entry.State = EntityState.Modified;

                    break;
            }

        foreach (var entry in ChangeTracker.Entries<Auditable>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedById = _currentUserService.UserId;
                    entry.Entity.Created = _dateTimeService.Now;
                    entry.Entity.LastModifiedById = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTimeService.Now;

                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedById = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTimeService.Now;

                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }

    #endregion
}