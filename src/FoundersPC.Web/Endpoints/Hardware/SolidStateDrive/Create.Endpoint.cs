using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.SolidStateDrive;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.SolidStateDrive;

public class CreateEndpoint : BaseRequestResponseManagementEndpoint<CreateRequest, SolidStateDriveInfo, GetEndpoint>
{
    [HttpPost("Hardware/SolidStateDrive")]
    [OpenApiOperation(operationId : "Hardware.SolidStateDrive.Post",
                      summary : "Create Hardware.SolidStateDrive",
                      description : "Create Hardware.SolidStateDrive")]
    [OpenApiTags("Hardware", "SolidStateDrive")]
    public override async Task<ActionResult<SolidStateDriveInfo>> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}