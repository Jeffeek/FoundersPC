using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.SharedKernel.Extensions;

public static class DbContextExtensions
{
    public static Task<TProjection> ProjectFirstAsNoTrackingAsync<TEntity, TProjection>(this DbContext context, IConfigurationProvider configurationProvider, Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        where TEntity : class
        where TProjection : class =>
        ProjectAsNoTrackingAsync<TEntity, TProjection>(context,
                                                       configurationProvider,
                                                       condition,
                                                       false,
                                                       cancellationToken)!;

    public static Task<TProjection?> ProjectFirstOrDefaultAsNoTrackingAsync<TEntity, TProjection>(this DbContext context, IConfigurationProvider configurationProvider, Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        where TEntity : class
        where TProjection : class =>
        ProjectAsNoTrackingAsync<TEntity, TProjection>(context,
                                                       configurationProvider,
                                                       condition,
                                                       true,
                                                       cancellationToken);

    private static Task<TProjection?> ProjectAsNoTrackingAsync<TEntity, TProjection>(DbContext context,
                                                                                     IConfigurationProvider configurationProvider,
                                                                                     Expression<Func<TEntity, bool>> condition,
                                                                                     bool orDefault,
                                                                                     CancellationToken cancellationToken = default)
        where TEntity : class
        where TProjection : class
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (configurationProvider == null) throw new ArgumentNullException(nameof(configurationProvider));
        if (condition == null) throw new ArgumentNullException(nameof(condition));

        var queryable = context.Set<TEntity>()
                               .AsNoTracking()
                               .Where(condition)
                               .ProjectTo<TProjection>(configurationProvider);

        return (orDefault
                    ? queryable.FirstOrDefaultAsync(cancellationToken)!
                    : queryable.FirstAsync(cancellationToken))!;
    }
}