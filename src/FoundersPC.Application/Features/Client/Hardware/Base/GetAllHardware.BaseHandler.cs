using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.Client.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public abstract class GetAllHardwareHandler<TGetAllRequest, TResponse, TGetAllQuery, THardware> : IRequestHandler<TGetAllRequest, IPagedList<TResponse>>
    where TGetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<TResponse>>
    where TResponse : ClientHardwareInfo
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

    public async Task<IPagedList<TResponse>> Handle(TGetAllRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<THardware>()
                       .AsNoTracking()
                       .ApplyQuery(_mapper.Map<TGetAllQuery>(request))
                       .ProjectTo<TResponse>(_mapper.ConfigurationProvider)
                       .ApplyPagingAsync(request, cancellationToken);
    }
}