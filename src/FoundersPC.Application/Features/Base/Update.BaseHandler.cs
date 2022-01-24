using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Base;

public abstract class UpdateHandler<TRequest, TResponse, TEntity, TGetQuery> : IRequestHandler<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
    where TEntity : class
    where TGetQuery : IQuery<TEntity>
{
    protected readonly IDbContextFactory<ApplicationDbContext> DbContextFactory;
    protected readonly IMapper Mapper;

    protected UpdateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
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

        _ = await db.Set<TEntity>()
                    .ApplyQuery(getQuery)
                    .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(GetNotFoundString(request));

        await db.Set<TEntity>()
                .Persist(Mapper)
                .InsertOrUpdateAsync(request, cancellationToken);

        await db.CommitTransactionAsync(cancellationToken);

        return await db.ProjectFirstAsNoTrackingAsync<TEntity, TResponse>(Mapper.ConfigurationProvider,
                                                                          getQuery.GetExpression(),
                                                                          cancellationToken);
    }

    protected abstract string GetNotFoundString(TRequest request);
}