using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.Case;
using FoundersPC.Application.Features.Client.Hardware.Case.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.Case;

public class GetEndpoint : BaseRequestResponseAccessTokenEndpoint<GetRequest, ClientCaseInfo, GetEndpoint>
{
    [HttpGet("Hardware/Case/{Id:int:required}")]
    [OpenApiOperation(operationId : "Client.Hardware.Case.Get",
                      summary : "Get ClientHardware.Case",
                      description : "Get Client.Hardware.Case")]
    [OpenApiTags("Hardware", "Case", "Client")]
    public override async Task<ClientCaseInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);
}