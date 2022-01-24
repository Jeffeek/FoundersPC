using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.HardDriveDisk;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.HardDriveDisk;

public class UpdateEndpoint : BaseRequestResponseManagementEndpoint<UpdateRequest, HardDriveDiskInfo, GetEndpoint>
{
    [HttpPut("Hardware/HardDriveDisk")]
    [OpenApiOperation(operationId : "Hardware.HardDriveDisk.Put",
                      summary : "Update Hardware.HardDriveDisk",
                      description : "Update Hardware.HardDriveDisk")]
    [OpenApiTags("Hardware", "HardDriveDisk")]
    public override async Task<ActionResult<HardDriveDiskInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}