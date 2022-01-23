using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.VideoCard.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Hardware.VideoCard;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, ClientVideoCardInfo, GetAllQuery, Domain.Entities.Hardware.VideoCard>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}