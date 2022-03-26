using AutoMapper;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class RestoreHandler : Base.RestoreHardwareHandler<RestoreRequest, Domain.Entities.Hardware.VideoCard, VideoCardInfo>
{
    public RestoreHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                          IMapper mapper)
        : base(dbContextFactory,
               mapper) { }
}