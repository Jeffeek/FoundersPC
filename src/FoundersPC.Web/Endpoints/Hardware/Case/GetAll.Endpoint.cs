using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Case;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.SharedKernel.Pagination;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Case;

public class GetAllEndpoint : BaseRequestResponseAnonymousEndpoint<GetAllRequest, IPagedList<CaseViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/Case/GetAll")]
    [OpenApiOperation(operationId : "Case.GetAll",
                      summary : "Get all cases",
                      description : "Get all cases")]
    [OpenApiTags("Hardware", "Case")]
    public override async Task<ActionResult<IPagedList<CaseViewInfo>>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}