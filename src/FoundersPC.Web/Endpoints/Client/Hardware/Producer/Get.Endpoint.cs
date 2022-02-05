using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Producer;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.Producer;

public class GetEndpoint : BaseRequestResponseAccessTokenEndpoint<GetRequest, ProducerInfo, GetEndpoint>
{
    [HttpGet("Producer/{Id:int:required}")]
    [OpenApiOperation(operationId : "Producer.Get",
                      summary : "Get Producer",
                      description : "Get Producer")]
    [OpenApiTags("Client", "Producer")]
    public override async Task<ActionResult<ProducerInfo>> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}