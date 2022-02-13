using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Processor;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Processor;

public class CreateEndpoint : BaseRequestResponseManagementEndpoint<CreateRequest, ProcessorInfo, GetEndpoint>
{
    [HttpPost("Hardware/Processor")]
    [OpenApiOperation(operationId : "Hardware.Processor.Post",
                      summary : "Create Hardware.Processor",
                      description : "Create Hardware.Processor")]
    [OpenApiTags("Hardware", "Processor")]
    public override async Task<ProcessorInfo> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}