using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Processor;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Processor;

public class UpdateEndpoint : BaseRequestResponseManagementEndpoint<UpdateRequest, ProcessorInfo, GetEndpoint>
{
    [HttpPut("Hardware/Processor")]
    [OpenApiOperation(operationId : "Hardware.Processor.Put",
                      summary : "Update Hardware.Processor",
                      description : "Update Hardware.Processor")]
    [OpenApiTags("Hardware", "Processor")]
    public override async Task<ActionResult<ProcessorInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}