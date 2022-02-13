﻿using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Processor;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.Processor;

public class GetEndpoint : BaseRequestResponseAccessTokenEndpoint<GetRequest, ProcessorInfo, GetEndpoint>
{
    [HttpGet("Hardware/Processor/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.Processor.Get",
                      summary : "Get Hardware.Processor",
                      description : "Get Hardware.Processor")]
    [OpenApiTags("Hardware", "Processor", "Client")]
    public override async Task<ProcessorInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}