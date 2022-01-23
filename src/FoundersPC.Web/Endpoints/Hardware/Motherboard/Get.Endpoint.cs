using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Motherboard;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Motherboard;

public class GetEndpoint : BaseRequestResponseManagementEndpoint<GetRequest, MotherboardInfo, GetEndpoint>
{
    [HttpGet("Hardware/Motherboard/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.Motherboard.Get",
                      summary : "Get Hardware.Motherboard",
                      description : "Get Hardware.Motherboard")]
    [OpenApiTags("Hardware", "Motherboard")]
    public override async Task<ActionResult<MotherboardInfo>> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}