using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.SharedKernel.Pagination;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.RandomAccessMemory;

public class GetAllEndpoint : BaseRequestResponseManagementEndpoint<GetAllRequest, IPagedList<RandomAccessMemoryViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/RandomAccessMemory/GetAll")]
    [OpenApiOperation(operationId : "RandomAccessMemory.GetAll",
                      summary : "Get all RandomAccessMemory",
                      description : "Get all RandomAccessMemory")]
    [OpenApiTags("Hardware", "RandomAccessMemory")]
    public override async Task<ActionResult<IPagedList<RandomAccessMemoryViewInfo>>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}