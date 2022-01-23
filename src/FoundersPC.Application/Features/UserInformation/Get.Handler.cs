using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.UserInformation;

public class GetHandler : IRequestHandler<GetRequest, UserInfo>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      ICurrentUserService currentUserService,
                      IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<UserInfo> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var userInfo = await db.Set<ApplicationUser>()
                               .Where(x => request.Id != null ? x.Id == request.Id : x.Login == request.Login)
                               .ProjectTo<UserInfo>(_mapper.ConfigurationProvider)
                               .FirstAsync(cancellationToken);

        if (_currentUserService.UserId == userInfo.Id)
            return userInfo;

        userInfo.Tokens.ForEach(x =>
                                {
                                    var splitLength = x.Token.Length / 2;
                                    x.Token = $"{x.Token[..splitLength]}{new string('*', splitLength)}";
                                });

        return userInfo;
    }
}