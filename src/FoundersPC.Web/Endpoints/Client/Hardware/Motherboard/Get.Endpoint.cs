using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.Motherboard;
using FoundersPC.Application.Features.Client.Hardware.Motherboard.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.Motherboard;

public class GetEndpoint : BaseRequestResponseAccessTokenEndpoint<GetRequest, ClientMotherboardInfo, GetEndpoint>
{
    [HttpGet("Hardware/Motherboard/{Id:int:required}")]
    [OpenApiOperation(operationId : "Client.Hardware.Motherboard.Get",
                      summary : "Get Client.Hardware.Motherboard",
                      description : "Get Client.Hardware.Motherboard")]
    [OpenApiTags("Hardware", "Motherboard", "Client")]
    public override async Task<ClientMotherboardInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = default) =>
        await Mediator.Send(request, cancellationToken);
}