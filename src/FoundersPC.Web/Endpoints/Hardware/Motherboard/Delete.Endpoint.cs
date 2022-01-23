using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Motherboard;
using FoundersPC.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Motherboard;

public class DeleteEndpoint : BaseRequestResponseManagementEndpoint<DeleteRequest, Unit, GetEndpoint>
{
    [HttpDelete("Hardware/Motherboard")]
    [OpenApiOperation(operationId : "Hardware.Motherboard.Delete",
                      summary : "Delete Hardware.Motherboard",
                      description : "Delete Hardware.Motherboard")]
    [OpenApiTags("Hardware", "Motherboard")]
    public override async Task<ActionResult<Unit>> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}