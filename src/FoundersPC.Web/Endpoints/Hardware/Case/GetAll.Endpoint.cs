using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Case;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Case;

public class GetAllEndpoint : BaseRequestResponseManagementEndpoint<GetAllRequest, IPagedList<CaseViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/Case/GetAll")]
    [OpenApiOperation(operationId : "Case.GetAll",
                      summary : "Get all cases",
                      description : "Get all cases")]
    [OpenApiTags("Hardware", "Case")]
    public override async Task<IPagedList<CaseViewInfo>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}