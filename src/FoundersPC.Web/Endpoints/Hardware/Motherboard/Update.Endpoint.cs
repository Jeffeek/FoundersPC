using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Motherboard;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Motherboard;

public class UpdateEndpoint : BaseRequestResponseManagementEndpoint<UpdateRequest, MotherboardInfo, GetEndpoint>
{
    [HttpPut("Hardware/Motherboard")]
    [OpenApiOperation(operationId : "Hardware.Motherboard.Put",
                      summary : "Update Hardware.Motherboard",
                      description : "Update Hardware.Motherboard")]
    [OpenApiTags("Hardware", "Motherboard")]
    public override async Task<ActionResult<MotherboardInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}