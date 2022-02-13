using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk;
using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.HardDriveDisk;

public class GetEndpoint : BaseRequestResponseAccessTokenEndpoint<GetRequest, ClientHardDriveDiskInfo, GetEndpoint>
{
    [HttpGet("Hardware/HardDriveDisk/{Id:int:required}")]
    [OpenApiOperation(operationId : "Client.Hardware.HardDriveDisk.Get",
                      summary : "Get Client.Hardware.HardDriveDisk",
                      description : "Get Client.Hardware.HardDriveDisk")]
    [OpenApiTags("Hardware", "HardDriveDisk", "Client")]
    public override async Task<ClientHardDriveDiskInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = default) =>
        await Mediator.Send(request, cancellationToken);
}