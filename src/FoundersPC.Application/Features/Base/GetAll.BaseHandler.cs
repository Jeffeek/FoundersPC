using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Filter;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Base;

public abstract class GetAllHandler<TRequest, TResponse, TEntity, TQuery> : IRequestHandler<TRequest, IPagedList<TResponse>>
    where TRequest : SortedPagedFilter, IRequest<IPagedList<TResponse>>
    where TQuery : IQuery<TEntity>
    where TEntity : class
{
    protected readonly IDbContextFactory<ApplicationDbContext> DbContextFactory;
    protected readonly IMapper Mapper;

    protected GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                            IMapper mapper)
    {
        DbContextFactory = dbContextFactory;
        Mapper = mapper;
    }

    public virtual async Task<IPagedList<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await DbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<TEntity>()
                       .AsNoTracking()
                       .ApplyQuery(Mapper.Map<TQuery>(request))
                       .ProjectTo<TResponse>(Mapper.ConfigurationProvider)
                       .ApplyPagingAsync(request, cancellationToken);
    }
}