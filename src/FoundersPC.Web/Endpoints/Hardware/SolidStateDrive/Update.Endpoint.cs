using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.SolidStateDrive;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.SolidStateDrive;

public class UpdateEndpoint : BaseRequestResponseManagementEndpoint<UpdateRequest, SolidStateDriveInfo, GetEndpoint>
{
    [HttpPut("Hardware/SolidStateDrive")]
    [OpenApiOperation(operationId : "Hardware.SolidStateDrive.Put",
                      summary : "Update Hardware.SolidStateDrive",
                      description : "Update Hardware.SolidStateDrive")]
    [OpenApiTags("Hardware", "SolidStateDrive")]
    public override async Task<ActionResult<SolidStateDriveInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}