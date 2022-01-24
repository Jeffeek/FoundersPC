using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.Case;
using FoundersPC.Application.Features.Client.Hardware.Case.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.Case;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientCaseInfo>, GetEndpoint>
{
    [HttpPost("Hardware/Case/GetAll")]
    [OpenApiOperation(operationId : "Client.Case.GetAll",
                      summary : "Get all Client.cases",
                      description : "Get all Client.cases")]
    [OpenApiTags("Hardware", "Case", "Client")]
    public override async Task<ActionResult<IPagedList<ClientCaseInfo>>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}