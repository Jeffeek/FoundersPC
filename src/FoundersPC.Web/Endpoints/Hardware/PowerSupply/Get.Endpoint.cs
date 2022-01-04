﻿using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.PowerSupply;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.PowerSupply;

public class GetEndpoint : BaseRequestResponseAnonymousEndpoint<GetRequest, PowerSupplyInfo, GetEndpoint>
{
    [HttpGet("Hardware/PowerSupply/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.PowerSupply.Get",
                      summary : "Get Hardware.PowerSupply",
                      description : "Get Hardware.PowerSupply")]
    [OpenApiTags("Hardware", "PowerSupply")]
    public override async Task<ActionResult<PowerSupplyInfo>> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}