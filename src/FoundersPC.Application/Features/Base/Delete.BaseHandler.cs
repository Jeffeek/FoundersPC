using AutoMapper;
using FoundersPC.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Domain.Common;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Base;

public abstract class DeleteHandler<TRequest, TEntity, TGetQuery> : IRequestHandler<TRequest, Unit>
    where TEntity : FullAuditable
    where TRequest : class, IRequest<Unit>
    where TGetQuery : class, IQuery<TEntity>
{
    protected readonly IMapper Mapper;
    protected readonly IDbContextFactory<ApplicationDbContext> DbContextFactory;

    protected DeleteHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                            IMapper mapper)
    {
        DbContextFactory = dbContextFactory;
        Mapper = mapper;
    }

    public virtual async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await using var db = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync(cancellationToken);

        var entity = await db.Set<TEntity>()
                             .ApplyQuery(Mapper.Map<TGetQuery>(request))
                             .FirstOrDefaultAsync(cancellationToken)
                     ?? throw new NotFoundException(GetNotFoundString(request));

        db.Set<TEntity>()
          .Remove(entity);

        await db.CommitTransactionAsync(cancellationToken);

        return Unit.Value;
    }

    protected abstract string GetNotFoundString(TRequest request);
}