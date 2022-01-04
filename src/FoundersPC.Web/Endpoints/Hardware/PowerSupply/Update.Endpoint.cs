using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.PowerSupply;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.PowerSupply;

public class UpdateEndpoint : BaseRequestResponseAnonymousEndpoint<UpdateRequest, PowerSupplyInfo, GetEndpoint>
{
    [HttpPut("Hardware/PowerSupply")]
    [OpenApiOperation(operationId : "Hardware.PowerSupply.Put",
                      summary : "Update Hardware.PowerSupply",
                      description : "Update Hardware.PowerSupply")]
    [OpenApiTags("Hardware", "PowerSupply")]
    public override async Task<ActionResult<PowerSupplyInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}