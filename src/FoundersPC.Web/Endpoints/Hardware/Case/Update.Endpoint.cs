using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Case;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Case;

public class UpdateEndpoint : BaseRequestResponseAnonymousEndpoint<UpdateRequest, CaseInfo, GetEndpoint>
{
    [HttpPut("Hardware/Case")]
    [OpenApiOperation(operationId : "Hardware.Case.Put",
                      summary : "Update Hardware.Case",
                      description : "Update Hardware.Case")]
    [OpenApiTags("Hardware", "Case")]
    public override async Task<ActionResult<CaseInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}