﻿using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.VideoCard;
using FoundersPC.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.VideoCard;

public class DeleteEndpoint : BaseRequestResponseAnonymousEndpoint<DeleteRequest, Unit, GetEndpoint>
{
    [HttpDelete("Hardware/VideoCard")]
    [OpenApiOperation(operationId : "Hardware.VideoCard.Delete",
                      summary : "Delete Hardware.VideoCard",
                      description : "Delete Hardware.VideoCard")]
    [OpenApiTags("Hardware", "VideoCard")]
    public override async Task<ActionResult<Unit>> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}