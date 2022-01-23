using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Processor;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.SharedKernel.Pagination;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Processor;

public class GetAllEndpoint : BaseRequestResponseManagementEndpoint<GetAllRequest, IPagedList<ProcessorViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/Processor/GetAll")]
    [OpenApiOperation(operationId : "Processor.GetAll",
                      summary : "Get all Processors",
                      description : "Get all Processors")]
    [OpenApiTags("Hardware", "Processor")]
    public override async Task<ActionResult<IPagedList<ProcessorViewInfo>>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}