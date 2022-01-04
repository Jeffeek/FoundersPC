﻿using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.SolidStateDrive;
using FoundersPC.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.SolidStateDrive;

public class DeleteEndpoint : BaseRequestResponseAnonymousEndpoint<DeleteRequest, Unit, GetEndpoint>
{
    [HttpDelete("Hardware/SolidStateDrive")]
    [OpenApiOperation(operationId : "Hardware.SolidStateDrive.Delete",
                      summary : "Delete Hardware.SolidStateDrive",
                      description : "Delete Hardware.SolidStateDrive")]
    [OpenApiTags("Hardware", "SolidStateDrive")]
    public override async Task<ActionResult<Unit>> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}