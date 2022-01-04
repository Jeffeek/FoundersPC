using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class UpdateHandler : UpdateHardwareHandler<UpdateRequest, VideoCardInfo, GetQuery, Domain.Entities.Hardware.VideoCard>
{
    public UpdateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}