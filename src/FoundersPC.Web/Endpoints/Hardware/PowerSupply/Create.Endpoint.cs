using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.PowerSupply;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.PowerSupply;

public class CreateEndpoint : BaseRequestResponseManagementEndpoint<CreateRequest, PowerSupplyInfo, GetEndpoint>
{
    [HttpPost("Hardware/PowerSupply")]
    [OpenApiOperation(operationId : "Hardware.PowerSupply.Post",
                      summary : "Create Hardware.PowerSupply",
                      description : "Create Hardware.PowerSupply")]
    [OpenApiTags("Hardware", "PowerSupply")]
    public override async Task<ActionResult<PowerSupplyInfo>> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}