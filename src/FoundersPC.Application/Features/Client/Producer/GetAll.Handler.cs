using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.Client.Producer;
using FoundersPC.Application.Features.Client.Producer.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Producer;

public class GetAllHandler : IRequestHandler<GetAllRequest, IPagedList<ClientProducerInfo>>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<IPagedList<ClientProducerInfo>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<Domain.Entities.Hardware.Producer>()
                       .AsNoTracking()
                       .ApplyQuery(_mapper.Map<GetAllQuery>(request))
                       .ProjectTo<ClientProducerInfo>(_mapper.ConfigurationProvider)
                       .ApplyPagingAsync(request, cancellationToken);
    }
}