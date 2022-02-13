using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Case;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Case;

public class GetEndpoint : BaseRequestResponseManagementEndpoint<GetRequest, CaseInfo, GetEndpoint>
{
    [HttpGet("Hardware/Case/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.Case.Get",
                      summary : "Get Hardware.Case",
                      description : "Get Hardware.Case")]
    [OpenApiTags("Hardware", "Case")]
    public override async Task<CaseInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}