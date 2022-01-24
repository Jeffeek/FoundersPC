using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class DeleteHandler : DeleteHandler<DeleteRequest, Domain.Entities.Hardware.Producer, GetQuery>
{
    public DeleteHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory, mapper) { }

    protected override string GetNotFoundString(DeleteRequest request) =>
        $"Not found producer with id {request.Id}";
}