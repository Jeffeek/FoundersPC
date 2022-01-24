using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Client.Producer.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Client.Producer;

public class GetAllHandler : GetAllHandler<GetAllRequest, ClientProducerInfo, Domain.Entities.Hardware.Producer, GetAllQuery>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory, mapper) { }
}