using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive;
using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.SolidStateDrive;

public class GetEndpoint : BaseRequestResponseAccessTokenEndpoint<GetRequest, ClientSolidStateDriveInfo, GetEndpoint>
{
    [HttpGet("Hardware/SolidStateDrive/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.SolidStateDrive.Get",
                      summary : "Get Hardware.SolidStateDrive",
                      description : "Get Hardware.SolidStateDrive")]
    [OpenApiTags("Hardware", "SolidStateDrive", "Client")]
    public override async Task<ClientSolidStateDriveInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}