using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class GetAllHandler : GetAllHandler<GetAllRequest, ProducerViewInfo, Domain.Entities.Hardware.Producer, GetAllQuery>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory, mapper) { }
}