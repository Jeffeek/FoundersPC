using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Producer;
using FoundersPC.Application.Features.Client.Producer.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Producer;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientProducerInfo>, GetAllEndpoint>
{
    [HttpPost("Producer/GetAll")]
    [OpenApiOperation(operationId : "Producer.GetAll",
                      summary : "Get all Producers",
                      description : "Get all Producers")]
    [OpenApiTags("Producer", "Client")]
    public override async Task<IPagedList<ClientProducerInfo>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}