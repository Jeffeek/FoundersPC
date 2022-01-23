using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.UserInformation;

public class GetAllHandler : IRequestHandler<GetAllRequest, IPagedList<UserViewInfo>>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<IPagedList<UserViewInfo>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await db.Set<ApplicationUser>()
                       .AsNoTracking()
                       .ApplyQuery(_mapper.Map<GetAllQuery>(request))
                       .ProjectTo<UserViewInfo>(_mapper.ConfigurationProvider)
                       .ApplyPagingAsync(request, cancellationToken);
    }
}