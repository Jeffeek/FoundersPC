using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Processor;
using FoundersPC.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Processor;

public class DeleteEndpoint : BaseRequestResponseAnonymousEndpoint<DeleteRequest, Unit, GetEndpoint>
{
    [HttpDelete("Hardware/Processor")]
    [OpenApiOperation(operationId : "Hardware.Processor.Delete",
                      summary : "Delete Hardware.Processor",
                      description : "Delete Hardware.Processor")]
    [OpenApiTags("Hardware", "Processor")]
    public override async Task<ActionResult<Unit>> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}