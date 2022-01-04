using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class GetHandler : GetHardwareHandler<GetRequest, VideoCardInfo, GetQuery, Domain.Entities.Hardware.VideoCard>
{
    public GetHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                      IMapper mapper) : base(dbContextFactory, mapper) { }
}