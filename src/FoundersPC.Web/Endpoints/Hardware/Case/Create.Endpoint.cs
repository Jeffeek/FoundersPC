﻿using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Case;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Case;

public class CreateEndpoint : BaseRequestResponseAnonymousEndpoint<CreateRequest, CaseInfo, GetEndpoint>
{
    [HttpPost("Hardware/Case")]
    [OpenApiOperation(operationId : "Hardware.Case.Post",
                      summary : "Create Hardware.Case",
                      description : "Create Hardware.Case")]
    [OpenApiTags("Hardware", "Case")]
    public override async Task<ActionResult<CaseInfo>> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}