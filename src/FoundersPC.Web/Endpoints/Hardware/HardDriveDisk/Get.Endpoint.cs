using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.HardDriveDisk;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.HardDriveDisk;

public class GetEndpoint : BaseRequestResponseManagementEndpoint<GetRequest, HardDriveDiskInfo, GetEndpoint>
{
    [HttpGet("Hardware/HardDriveDisk/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.HardDriveDisk.Get",
                      summary : "Get Hardware.HardDriveDisk",
                      description : "Get Hardware.HardDriveDisk")]
    [OpenApiTags("Hardware", "HardDriveDisk")]
    public override async Task<ActionResult<HardDriveDiskInfo>> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}