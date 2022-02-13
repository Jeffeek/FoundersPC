using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Case;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Case;

public class UpdateEndpoint : BaseRequestResponseManagementEndpoint<UpdateRequest, CaseInfo, GetEndpoint>
{
    [HttpPut("Hardware/Case")]
    [OpenApiOperation(operationId : "Hardware.Case.Put",
                      summary : "Update Hardware.Case",
                      description : "Update Hardware.Case")]
    [OpenApiTags("Hardware", "Case")]
    public override async Task<CaseInfo> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}