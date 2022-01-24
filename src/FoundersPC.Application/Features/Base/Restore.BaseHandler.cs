using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Domain.Common;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Base;

public abstract class RestoreHandler<TRequest, TResponse, TEntity, TGetQuery> : IRequestHandler<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
    where TEntity : FullAuditable
    where TGetQuery : class, IQuery<TEntity>
{
    protected readonly IDbContextFactory<ApplicationDbContext> DbContextFactory;
    protected readonly IMapper Mapper;

    protected RestoreHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                             IMapper mapper)
    {
        DbContextFactory = dbContextFactory;
        Mapper = mapper;
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);
        var getQuery = Mapper.Map<TGetQuery>(request);

        var entity = await db.Set<TEntity>()
                             .ApplyQuery(getQuery)
                             .FirstOrDefaultAsync(cancellationToken)
                     ?? throw new NotFoundException(GetNotFoundString(request));

        entity.IsDeleted = false;
        entity.DeletedById = null;
        entity.Deleted = null;

        await db.CommitTransactionAsync(cancellationToken);

        return await db.ProjectFirstAsNoTrackingAsync<TEntity, TResponse>(Mapper.ConfigurationProvider,
                                                                          getQuery.GetExpression(),
                                                                          cancellationToken);
    }

    protected abstract string GetNotFoundString(TRequest request);
}