using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class UpdateHandler : UpdateHandler<UpdateRequest, ProducerInfo, Domain.Entities.Hardware.Producer, GetQuery>
{
    public UpdateHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory, mapper) { }

    protected override string GetNotFoundString(UpdateRequest request) =>
        $"Not found producer with id {request.Id}";
}