using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.HardDriveDisk;
using FoundersPC.Web.Endpoints.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.HardDriveDisk;

public class DeleteEndpoint : BaseRequestResponseManagementEndpoint<DeleteRequest, Unit, GetEndpoint>
{
    [HttpDelete("Hardware/HardDriveDisk")]
    [OpenApiOperation(operationId : "Hardware.HardDriveDisk.Delete",
                      summary : "Delete Hardware.HardDriveDisk",
                      description : "Delete Hardware.HardDriveDisk")]
    [OpenApiTags("Hardware", "HardDriveDisk")]
    public override async Task<ActionResult<Unit>> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}