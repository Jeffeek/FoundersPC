using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.PowerSupply;
using FoundersPC.Web.Endpoints.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.PowerSupply;

public class DeleteEndpoint : BaseRequestResponseManagementEndpoint<DeleteRequest, Unit, GetEndpoint>
{
    [HttpDelete("Hardware/PowerSupply")]
    [OpenApiOperation(operationId : "Hardware.PowerSupply.Delete",
                      summary : "Delete Hardware.PowerSupply",
                      description : "Delete Hardware.PowerSupply")]
    [OpenApiTags("Hardware", "PowerSupply")]
    public override async Task<ActionResult<Unit>> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}