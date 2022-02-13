using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.HardDriveDisk;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.HardDriveDisk;

public class CreateEndpoint : BaseRequestResponseManagementEndpoint<CreateRequest, HardDriveDiskInfo, GetEndpoint>
{
    [HttpPost("Hardware/HardDriveDisk")]
    [OpenApiOperation(operationId : "Hardware.HardDriveDisk.Post",
                      summary : "Create Hardware.HardDriveDisk",
                      description : "Create Hardware.HardDriveDisk")]
    [OpenApiTags("Hardware", "HardDriveDisk")]
    public override async Task<HardDriveDiskInfo> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}