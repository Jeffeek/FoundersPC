using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetAllHardwareHandler<TGetAllRequest, THardwareViewResponse, TGetAllQuery, THardware> : IRequestHandler<TGetAllRequest, IPagedList<THardwareViewResponse>>
    where TGetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<THardwareViewResponse>>
    where THardwareViewResponse : HardwareViewInfo
    where TGetAllQuery : SortedQuery<THardware>
    where THardware : Domain.Entities.Hardware.Hardware
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    protected GetAllHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<IPagedList<THardwareViewResponse>> Handle(TGetAllRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<THardware>()
                       .AsNoTracking()
                       .ApplyQuery(_mapper.Map<TGetAllQuery>(request))
                       .ProjectTo<THardwareViewResponse>(_mapper.ConfigurationProvider)
                       .ApplyPagingAsync(request, cancellationToken);
    }
}