using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Base;

public abstract class NullableGetHandler<TRequest, TResponse, TEntity, TQuery> : IRequestHandler<TRequest, TResponse?>
    where TRequest : class, IRequest<TResponse?>
    where TResponse : class
    where TEntity : class
    where TQuery : class, IQuery<TEntity>
{
    protected readonly IMapper Mapper;
    protected readonly IDbContextFactory<ApplicationDbContext> DbContextFactory;

    protected NullableGetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                 IMapper mapper)
    {
        DbContextFactory = dbContextFactory;
        Mapper = mapper;
    }

    public virtual async Task<TResponse?> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await DbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.ProjectFirstOrDefaultAsNoTrackingAsync<TEntity, TResponse>(Mapper.ConfigurationProvider,
                                                                                   Mapper.Map<TQuery>(request)
                                                                                         .GetExpression(),
                                                                                   cancellationToken);
    }
}

public abstract class NotNullableGetHandler<TRequest, TResponse, TEntity, TQuery> : IRequestHandler<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
    where TEntity : class
    where TQuery : class, IQuery<TEntity>
{
    protected readonly IMapper Mapper;
    protected readonly IDbContextFactory<ApplicationDbContext> DbContextFactory;

    protected NotNullableGetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper)
    {
        DbContextFactory = dbContextFactory;
        Mapper = mapper;
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await DbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.ProjectFirstAsNoTrackingAsync<TEntity, TResponse>(Mapper.ConfigurationProvider,
                                                                          Mapper.Map<TQuery>(request)
                                                                                .GetExpression(),
                                                                          cancellationToken);
    }
}